namespace NftFaucet.Models;

public class IpfsBlockchainContext
{
    public string Address { get; private set; }
    public string SignedMessage { get; private set; }

    public bool IsInitialized => !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(SignedMessage);

    public void Initialize(string address, string signedMessage)
    {
        if (string.IsNullOrEmpty(address))
            throw new ArgumentNullException(nameof(address));

        if (string.IsNullOrEmpty(signedMessage))
            throw new ArgumentNullException(nameof(signedMessage));

        Address = address;
        SignedMessage = signedMessage;
    }
}
