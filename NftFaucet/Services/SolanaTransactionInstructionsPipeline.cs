using Solana.Metaplex;
using Solnet.Programs;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Models;
using Solnet.Wallet;

namespace NftFaucet.Services;

public class SolanaTransactionInstructionsPipeline
{
    private readonly List<TransactionInstruction> _instructions = new List<TransactionInstruction>();

    public void InitializeForMint(PublicKey from, PublicKey dest, PublicKey mint, ulong balanceForMint, ulong tokenAmount, ulong tokenPrice)
    {
        Add(SystemProgram.CreateAccount(from, mint, balanceForMint, TokenProgram.MintAccountDataSize, TokenProgram.ProgramIdKey));
        Add(TokenProgram.InitializeMint(mint, 0, from, from));
        Add(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(from, dest, mint));

        var tokenBalanceAddress = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(dest, mint);

        Add(TokenProgram.MintTo(mint, tokenBalanceAddress, tokenAmount, from));
        Add(SystemProgram.Transfer(from, from, tokenPrice));
    }

    public void AddMetadata(PublicKey from, PublicKey mint, PublicKey metadataAddress, MetadataParameters data, bool isMutable = true)
    {
        Add(MetadataProgram.CreateMetadataAccount(
            metadataAddress,
            mint,
            from,
            from,
            from,
            data,
            true,
            isMutable
        ));
    }

    public void AddMasterEdition(PublicKey from, PublicKey mint, PublicKey masterEditionAddress, PublicKey metadataAddress, MetadataParameters data)
    {
        Add(MetadataProgram.CreateMasterEdition(
            1,
            masterEditionAddress,
            mint,
            from,
            from,
            from,
            metadataAddress
        ));
    }

    public void AddRange(TransactionInstruction[] instructions)
    {
        _instructions.AddRange(instructions);
    }

    public void Add(TransactionInstruction instruction)
    {
        _instructions.Add(instruction);
    }

    public void Build(TransactionBuilder builder)
    {
        _instructions.ForEach(x => builder.AddInstruction(x));
    }
}

