namespace NftFaucet.Models.Enums;

public enum NetworkChain : long
{
    EthereumMainnet = 1,
    Ropsten = 3,
    Rinkeby = 4,
    Goerli = 5,
    Kovan = 42,
    OptimismMainnet = 10,
    OptimismKovan = 69,
    PolygonMainnet = 137,
    PolygonMumbai = 80001,
    MoonbeamMainnet = 1284,
    MoonbaseAlpha = 1287,
    ArbitrumMainnetBeta = 42161,
    ArbitrumRinkeby = 421611,
    ArbitrumGoerli = 421612,
    AvalancheMainnet = 43114,
    AvalancheFuji = 43113,
    
    // BNB Smart Chain
    BnbChainMainnet = 56,
    BnbChainTestnet = 97,

    // solana
    SolanaMainnet = 11100,
    SolanaDevnet = 11101,
    SolanaTestnet = 11110,
}
