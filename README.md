# NFT Faucet
[![Deploy to GitHub Pages](https://github.com/darkcodi/nft-faucet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/darkcodi/nft-faucet/actions/workflows/main.yml)  
  
It's a WASM web-application that allows you to mint ERC-721 and ERC-1155 tokens to any specified ethereum address.
  
## Requirements
- installed [Metamask](https://metamask.io/download/) extension
- some test ETH (or MATIC) to pay for a blockchain transaction fee  
(faucets: [Ropsten](https://faucet.egorfine.com/), [Mumbai](https://mumbaifaucet.com/), etc.)
  
## Demo
Go to https://darkcodi.github.io/nft-faucet/  
  
[![demo](demo.gif)]()  

## Deployed contracts
Used [contracts](NftFaucet/Contracts) are based on [OpenZeppilin contracts](https://github.com/OpenZeppelin/openzeppelin-contracts), but with one unusual feature - `mint` method can be called by anyone, not just by an owner.    

|                | ERC-721   | ERC-1155   |
|----------------|-----------|------------|
| Ropsten        | [0x71902F99902339d7ce1F994C12155f4350BCD226](https://ropsten.etherscan.io/token/0x71902F99902339d7ce1F994C12155f4350BCD226) | [0x80b45421881c0452A6e70148Fc928fA33107cEb3](https://ropsten.etherscan.io/token/0x80b45421881c0452A6e70148Fc928fA33107cEb3) |
| Polygon Mumbai | [0xeE8272220A0988279627714144Ff6981E204fbE4](https://mumbai.polygonscan.com/token/0xeE8272220A0988279627714144Ff6981E204fbE4) | [0x23147CdbD963A3D0fec0F25E4604844f477F65d2](https://mumbai.polygonscan.com/token/0x23147CdbD963A3D0fec0F25E4604844f477F65d2) |
  
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
