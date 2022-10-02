using Cryptography.ECDSA;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;

namespace NftFaucet.UploadPlugins.Crust;

public class EthereumKey
{
    public string PrivateKey { get; }
    public string Address { get; }

    public EthereumKey(string privateKey)
    {
        PrivateKey = privateKey ?? throw new ArgumentNullException(nameof(privateKey));
        Address = GetAddressFromPrivateKey(privateKey);
    }

    public static EthereumKey GenerateNew()
    {
        var privateKeyBytes = Secp256K1Manager.GenerateRandomKey();
        var privateKeyString = privateKeyBytes.ToHex(prefix: false);
        return new EthereumKey(privateKeyString);
    }

    public static string GetAddressFromPrivateKey(string privateKey)
    {
        var privateKeyBytes = privateKey.HexToByteArray();
        var publicKeyBytes = Secp256K1Manager.GetPublicKey(privateKeyBytes, false).Skip(1).ToArray();
        var publicKeyHash = new Sha3Keccack().CalculateHash(publicKeyBytes);
        var addressBytes = new byte[publicKeyHash.Length - 12];
        Array.Copy(publicKeyHash, 12, addressBytes, 0, publicKeyHash.Length - 12);
        var addressString = new AddressUtil().ConvertToChecksumAddress(addressBytes.ToHex());
        return addressString;
    } 
}
