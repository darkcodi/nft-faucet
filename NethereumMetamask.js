async function metamaskRequest(parsedMessage) {
    try {
        const response = await ethereum.request(parsedMessage);
        let rpcResponse = {
            jsonrpc: "2.0",
            result: response,
            id: parsedMessage.id,
            error: null
        }

        return rpcResponse;
    } catch (e) {
        console.log(e);
        let rpcResonseError = {
            jsonrpc: "2.0",
            id: parsedMessage.id,
            error: e
        }
        return rpcResonseError;
    }
}

async function getSelectedAddress() {
    let accountsReponse = await getAddresses();
    if (accountsReponse.error !== null) throw accountsReponse.error;
    return accountsReponse.result[0];
}

async function getAddresses() {
   return await metamaskRequest({ method: 'eth_requestAccounts' });
}

window.NethereumMetamaskInterop = {
    Sign: async (utf8HexMsg) => {
        try {
            const from = await getSelectedAddress();
            const params = [utf8HexMsg, from];
            const method = 'personal_sign';
            const rpcResponse = await metamaskRequest({
                method,
                params,
                from
            });
            return JSON.stringify(rpcResponse);
        } catch (e) {
            console.log(e);
            let rpcResponseError = {
                jsonrpc: "2.0",
                id: parsedMessage.id,
                error: e
            }
            return JSON.stringify(rpcResponseError);
        }
    }
}
