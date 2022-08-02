using Microsoft.JSInterop;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using RpcError = Nethereum.JsonRpc.Client.RpcError;

namespace NftFaucet.Services;

public class MetamaskSigningService
{
    private readonly IJSRuntime _jsRuntime;

    public MetamaskSigningService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> SignAsync(string message)
    {
        var rpcJsonResponse = await _jsRuntime.InvokeAsync<string>("NethereumMetamaskInterop.Sign", message.ToHexUTF8());
        var response = JsonConvert.DeserializeObject<RpcResponseMessage>(rpcJsonResponse);
        if (response.HasError)
        {
            var rpcError = new RpcError(response.Error.Code, response.Error.Message, response.Error.Data);
            throw new RpcResponseException(rpcError);
        }

        try
        {
            return response.GetResult<string>();
        }
        catch (FormatException formatException)
        {
            throw new RpcResponseFormatException("Invalid format found in RPC response", formatException);
        }
    }
}
