using Solnet.Wallet;
using Solnet.Wallet.Bip39;

namespace NftFaucet.Models;

public class SolanaKey
{
    public string MnemonicPhrase { get; }
    public string PrivateKey { get; }
    public string Address { get; }
    
    public SolanaKey(string mnemonicPhrase)
    {
        MnemonicPhrase = mnemonicPhrase ?? throw new ArgumentNullException(nameof(mnemonicPhrase));
        PrivateKey = GetPrivateKeyFromMnemonicPhrase(mnemonicPhrase);
        Address = GetAddressFromMnemonicPhrase(mnemonicPhrase);
    }

    public static SolanaKey GenerateNew()
    {
        var words = new Mnemonic(WordList.English, WordCount.Twelve).Words;
        var mnemonicPhrase = string.Join(" ", words);
        return new SolanaKey(mnemonicPhrase);
    }
    
    public static string GetPrivateKeyFromMnemonicPhrase(string mnemonicPhrase)
    {
        var mnemonic = new Mnemonic(mnemonicPhrase, WordList.English);
        var wallet = new Wallet(mnemonic);
        return wallet.Account.PrivateKey;
    }

    public static string GetAddressFromMnemonicPhrase(string mnemonicPhrase)
    {
        var mnemonic = new Mnemonic(mnemonicPhrase, WordList.English);
        var wallet = new Wallet(mnemonic);
        return wallet.Account.PublicKey.Key;
    } 
}
