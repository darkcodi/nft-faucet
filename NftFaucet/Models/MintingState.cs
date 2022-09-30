namespace NftFaucet.Models;

public enum MintingState
{
    CheckingNetwork,
    CheckingAddress,
    CheckingBalance,
    SendingTransaction,
    Done,
}
