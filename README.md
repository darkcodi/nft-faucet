# NFT Faucet
[![Deploy to GitHub Pages](https://github.com/darkcodi/nft-faucet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/darkcodi/nft-faucet/actions/workflows/main.yml)  
  
It's a WASM web-application that allows you to mint ERC-721 and ERC-1155 tokens to any specified ethereum address.
  
## Demo
Go to https://darkcodi.github.io/nft-faucet/  
<TODO: add gif>
  
## Requirements
- installed [Metamask](https://metamask.io/download/) extension
- some test ETH (or MATIC) to pay for a blockchain transaction fee  
(faucets: [Ropsten](https://faucet.egorfine.com/), [Mumbai](https://mumbaifaucet.com/), etc.)
  
## Deployed contracts
Used [contracts](NftFaucet/Contracts) are based on [OpenZeppilin contracts](https://github.com/OpenZeppelin/openzeppelin-contracts), but with one unusual feature - `mint` method can be called by anyone, not just by an owner.    

|                | ERC-721   | ERC-1155   |
|----------------|-----------|------------|
| Ropsten        | [0x71902F99902339d7ce1F994C12155f4350BCD226](https://ropsten.etherscan.io/token/0x71902F99902339d7ce1F994C12155f4350BCD226) | [0x6c0449f3B8135f896637afd29c7aeA496ED6f4F3](https://ropsten.etherscan.io/token/0x6c0449f3B8135f896637afd29c7aeA496ED6f4F3) |
| Polygon Mumbai | [0xeE8272220A0988279627714144Ff6981E204fbE4](https://mumbai.polygonscan.com/token/0xeE8272220A0988279627714144Ff6981E204fbE4) | [0x2E6C3fa7B2Ed656e695A29276a22c8C33d118a1B](https://mumbai.polygonscan.com/token/0x2E6C3fa7B2Ed656e695A29276a22c8C33d118a1B) |
  
## Technology stack

NOTE: The entire web app works as a static website, hosted on Github Pages. There is NO backend, it runs only in your browser! :)  

- Blazor WASM
- Metamask
- IPFS (upload provider - Infura, pinning provider - Crust)
- Solidity smart contracts
  
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)  

## How to run it locally?
Simply type this command in the root of this repo:

    dotnet run --project NftFaucet
