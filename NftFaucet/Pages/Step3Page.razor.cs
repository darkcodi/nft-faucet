using System.Text;
using BlazorMonaco;
using Newtonsoft.Json;
using NftFaucet.Components;
using NftFaucet.Extensions;
using NftFaucet.Models.Token;

namespace NftFaucet.Pages;

public class Step3Component : BasicComponent
{
    protected MonacoEditor Editor { get; set; }
    protected string EditorErrorMessage { get; set; }
    protected string EditorClass => string.IsNullOrWhiteSpace(EditorErrorMessage) ? null : "invalid-input";

    protected StandaloneEditorConstructionOptions EditorConstructionOptions(MonacoEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            Language = "json",
            GlyphMargin = true,
            Value = GetCurrentMetadataJson(),
        };
    }

    protected override async Task OnInitializedAsync()
    {
        if (!await AppState.Metamask.IsReady() || !AppState.IpfsContext.IsInitialized ||  AppState.Storage.IpfsImageUrl == null)
            UriHelper.NavigateToRelative("/");

        AppState.Navigation.SetForwardHandler(ForwardHandler);

        await Task.Yield();
        await Editor.SetValue(GetCurrentMetadataJson());
    }

    protected async Task<bool> ForwardHandler()
    {
        var metadataJson = await Editor.GetValue();
        if (!metadataJson.IsValidJson())
        {
            EditorErrorMessage = "Invalid JSON";
            RefreshMediator.NotifyStateHasChangedSafe();
            return false;
        }

        if (AppState.Storage.TokenMetadata == metadataJson)
        {
            return true;
        }

        AppState.Storage.UploadIsInProgress = true;
        RefreshMediator.NotifyStateHasChangedSafe();

        var metadataBytes = Encoding.UTF8.GetBytes(metadataJson);
        var tokenUri = await IpfsService.Upload("token.json", "application/json", metadataBytes);
        tokenUri = IpfsService.GetUrlToGateway(tokenUri, AppState.Storage.IpfsGatewayType);
        AppState.Storage.TokenMetadata = metadataJson;
        AppState.Storage.TokenUrl = tokenUri.OriginalString;

        AppState.Storage.UploadIsInProgress = false;
        RefreshMediator.NotifyStateHasChangedSafe();

        return true;
    }

    private string GetCurrentMetadataJson()
    {
        var imageUrl = AppState?.Storage?.IpfsImageUrl != null
            ? IpfsService.GetUrlToGateway(AppState.Storage.IpfsImageUrl, AppState.Storage.IpfsGatewayType)
            : null;

        var metadata = new TokenMetadata
        {
            Name = AppState?.Storage?.TokenName,
            Description = AppState?.Storage?.TokenDescription,
            Image = imageUrl?.OriginalString,
            ExternalUrl = "https://darkcodi.github.io/nft-faucet/",
        };

        var metadataJson = JsonConvert.SerializeObject(metadata, Formatting.Indented);
        return metadataJson;
    }
}
