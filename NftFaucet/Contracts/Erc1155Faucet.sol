// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "@openzeppelin/contracts@4.5.0/access/AccessControlEnumerable.sol";
import "@openzeppelin/contracts@4.5.0/token/ERC1155/ERC1155.sol";
import "@openzeppelin/contracts@4.5.0/utils/Context.sol";
import "@openzeppelin/contracts@4.5.0/utils/Counters.sol";

contract Erc1155Faucet is Context, AccessControlEnumerable, ERC1155 {
    string internal nftName;
    string internal nftSymbol;

    using Counters for Counters.Counter;
    Counters.Counter private _tokenIdCounter;

    mapping(uint256 => string) internal _uriDict;

    constructor() ERC1155("https://token-cdn-domain/{id}.json")
    {
        _setupRole(DEFAULT_ADMIN_ROLE, _msgSender());
        nftName = "ERC-1155 Faucet";
        nftSymbol = "FA1155";
    }

    function mint(
        address to,
        uint256 amount,
        string calldata tokenUri)
        public
        virtual
    {
        uint256 tokenId = _tokenIdCounter.current();
        _tokenIdCounter.increment();
        _mint(to, tokenId, amount, "0x1234");
        _uriDict[tokenId] = tokenUri;
    }

    function uri(uint256 id)
        public
        view
        virtual
        override
        returns (string memory)
    {
        return _uriDict[id];
    }

    function name()
        external
        view
        returns (string memory _name)
    {
        _name = nftName;
    }

    function symbol()
        external
        view
        returns (string memory _symbol)
    {
        _symbol = nftSymbol;
    }

    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC1155, AccessControlEnumerable)
        returns (bool)
    {
        return ERC1155.supportsInterface(interfaceId) || AccessControlEnumerable.supportsInterface(interfaceId);
    }
}
