import { ethers } from "./ethers.min.js";

export function createMetaMaskObj() {
    if (typeof MetaMask === "undefined") {
        window.MetaMask = new MetaMaskService();
    }
}

export function setDotnetReference(dotNetRef) {
    if (typeof DotNetReference === "undefined") {
        window.DotNetReference = dotNetRef;
    }
}

export function bindEvents() {
    return MetaMask.bindEvents();
}

export function isMetaMaskAvailable() {
    return typeof ethereum !== "undefined";
}

export function isSiteConnected() {
    return (isMetaMaskAvailable() && ethereum.selectedAddress !== null);
}

export async function connect() {
    return await MetaMask.connect();
}

export async function changeAccount() {
    return await MetaMask.changeAccount();
}

export async function getSelectedAccount() {
    return await MetaMask.getSelectedAccount();
}

export async function getSelectedChain() {
    return await MetaMask.getSelectedChain();
}

export async function getBalance(address) {
    return await MetaMask.getBalance(address);
}

export async function getTokenBalance(tokenAddress, account) {
    return await MetaMask.getTokenBalance(tokenAddress, account);
}

export async function sendTransaction(to, value, data) {
    return await MetaMask.sendTransaction(to, value, data);
}

export async function requestRpc(method, params) {
    return await MetaMask.requestRpc(method, params);
}

export class MetaMaskService {
    constructor() {
        this.provider = new ethers.providers.Web3Provider(window.ethereum, "any");
    }

    bindEvents() {
        if (typeof DotNetReference !== "undefined" &&
            typeof ethereum !== "undefined")
        {
            ethereum.on("connect", async connectInfo => {
                await DotNetReference.invokeMethodAsync("OnConnect", connectInfo);
            });

            ethereum.on("disconnect", async error => {
                await DotNetReference.invokeMethodAsync("OnDisconnect", error);
            });

            ethereum.on("accountsChanged", async accounts => {
                await DotNetReference.invokeMethodAsync("OnAccountsChanged", accounts);
            });

            ethereum.on("message", async providerMessage => {
                await DotNetReference.invokeMethodAsync("OnMessageReceived", providerMessage);
            });

            ethereum.on("chainChanged", async chainId => {
                await DotNetReference.invokeMethodAsync("OnChainChanged", chainId);
                window.location.reload();
            });
        }
    }

    isMetaMaskAvailable() {
        return typeof ethereum !== "undefined";
    }

    isSiteConnected() {
        return (this.isMetaMaskAvailable() && ethereum.selectedAddress !== null)
    }

    async connect() {
        try {
            if (this.isMetaMaskAvailable() === false) {
                console.warn("Metamask is not available");
                return null;
            }

            let accounts = await ethereum.request({
                method: "eth_requestAccounts",
            });
            return accounts[0];
        } catch (e) {
            console.error(e);
            return null;
        }
    }

    async changeAccount() {
        try {
            if (this.isMetaMaskAvailable() === false) {
                console.warn("Metamask is not available");
                return null;
            }

            let permissions = await ethereum.request({
                method: "wallet_requestPermissions",
                params: [{ eth_accounts: {} }],
            });

            const accountsPermission = permissions.find(
                (permission) => permission.parentCapability === "eth_accounts"
            );

            if (accountsPermission) {
                return await getSelectedAccount();
            }

            return null;
        } catch (e) {
            console.error(e);
            return null;
        }
    }

    async getSelectedAccount() {
        let accounts = await ethereum.request({
            method: "eth_accounts"
        });

        return accounts[0];
    }

    async getSelectedChain() {
        return await ethereum.request({
            method: "eth_chainId"
        });
    }

    async getBalance(account) {
        if (!account) {
            account = await this.getSelectedAccount();
        }

        return await ethereum.request({
            method: "eth_getBalance",
            params: [account, "latest"]
        });
    }

    async getTokenBalance(tokenAddress, account) {
        const abi = [
            "function name() view returns (string)",
            "function symbol() view returns (string)",
            "function balanceOf(address) view returns (uint)",
        ];

        if (!account) {
            account = await this.getSelectedAccount();
        }

        try {
            const contract = new ethers.Contract(tokenAddress, abi, this.provider);
            let balance = await contract.balanceOf(account);
            return balance._hex;
        }
        catch (e) {
            console.error(e);
            return "0x0";
        }
    }

    async sendTransaction(to, value, data) {
        if (this.isSiteConnected() === false) {
            console.log("Could not connect to MetaMask")
            return null;
        }

        const transactionParameters = {
            to: to,
            from: await this.getSelectedAccount(),
            value: value,
            data: data
        };

        try {
            return await ethereum.request({
                method: "eth_sendTransaction",
                params: [transactionParameters]
            });
        } catch (e) {
            console.error(e);
            return null;
        }
    }

    async requestRpc(method, params) {
        try {
            const requestMessage = {
                method: method,
                params: params
            }

            const response = await ethereum.request(requestMessage);
            let rpcResponse = {
                jsonrpc: "2.0",
                result: response,
                //id: requestMessage.id,
                error: null
            }
            return JSON.stringify(rpcResponse);
        } catch (e) {
            let rpcResponseError = {
                jsonrpc: "2.0",
                //id: requestMessage.id,
                error: {
                    message: e,
                }
            }
            return JSON.stringify(rpcResponseError);
        }
    }
}