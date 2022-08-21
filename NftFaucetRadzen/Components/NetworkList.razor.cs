using Microsoft.AspNetCore.Components;
using NftFaucetRadzen.Models;

namespace NftFaucetRadzen.Components;

public partial class NetworkList
{
    [Parameter] public NetworkModel[] Data { get; set; }
}
