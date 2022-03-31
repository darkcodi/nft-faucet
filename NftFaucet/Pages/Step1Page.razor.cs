using AntDesign;
using Microsoft.AspNetCore.Components;
using NftFaucet.Components;
using NftFaucet.Constants;
using NftFaucet.Models.Enums;

namespace NftFaucet.Pages;

public class Step1Component : BasicComponent
{
    protected string NameErrorMessage { get; set; }
    protected string DescriptionErrorMessage { get; set; }
    protected string ImageErrorMessage { get; set; }
    protected string NameClass => string.IsNullOrWhiteSpace(NameErrorMessage) ? null : "invalid-input";
    protected string DescriptionClass => string.IsNullOrWhiteSpace(DescriptionErrorMessage) ? null : "invalid-input";
    protected string ImageClass => "file-uploader" + (string.IsNullOrWhiteSpace(ImageErrorMessage) ? string.Empty : " invalid-input");

    protected EnumWrapper<IpfsGatewayType>[] IpfsGateways { get; } = Enum.GetValues<IpfsGatewayType>()
        .Select(x => new EnumWrapper<IpfsGatewayType>(x, x.ToString())).ToArray();

    protected EnumWrapper<TokenType>[] TokenTypes { get; } = Enum.GetValues<TokenType>()
        .Select(x => new EnumWrapper<TokenType>(x, x.ToString())).ToArray();

    protected override async Task OnInitializedAsync()
    {
        if (!await AppState.Metamask.IsReady())
            UriHelper.NavigateTo("/");

        AppState.Navigation.SetForwardHandler(ForwardHandler);
    }

    protected Task<bool> ForwardHandler()
    {
        var isValidName = !string.IsNullOrWhiteSpace(AppState.Storage.TokenName);
        var isValidDescription = !string.IsNullOrWhiteSpace(AppState.Storage.TokenDescription);
        var isValidFile = AppState.Storage.IpfsImageUrl != null;
        var isNotUploading = !AppState.Storage.UploadIsInProgress;

        if (!isValidName)
        {
            NameErrorMessage = "Invalid name";
        }

        if (!isValidDescription)
        {
            DescriptionErrorMessage = "Invalid description";
        }

        if (!isValidFile)
        {
            ImageErrorMessage = "Invalid file";
        }

        if (!isNotUploading)
        {
            ImageErrorMessage = "Upload is still in progress";
        }

        RefreshMediator.NotifyStateHasChangedSafe();

        var canProceed = isValidName && isValidDescription && isValidFile && isNotUploading;
        return Task.FromResult(canProceed);
    }

    protected void OnNameInputChange(ChangeEventArgs args)
    {
        NameErrorMessage = string.Empty;
    }

    protected void OnDescriptionInputChange(ChangeEventArgs args)
    {
        DescriptionErrorMessage = string.Empty;
    }

    protected void OnIpfsGatewayChange(EnumWrapper<IpfsGatewayType> ipfsGatewayItem)
    {
        AppState.Storage.IpfsGatewayType = ipfsGatewayItem.Value;
    }

    protected void OnTokenTypeChange(EnumWrapper<TokenType> tokenTypeItem)
    {
        AppState.Storage.TokenType = tokenTypeItem.Value;
        if (AppState.Storage.TokenType == TokenType.ERC721)
        {
            AppState.Storage.TokenAmount = 1;
        }
        RefreshMediator.NotifyStateHasChangedSafe();
    }

    protected async Task<bool> BeforeUpload(List<UploadFileItem> files)
    {
        var file = files.FirstOrDefault();
        if (file == null)
        {
            return false;
        }

        var hasValidSize = file.Size < UploadConstants.MaxFileSizeInBytes;
        if (!hasValidSize)
        {
            MessageService.Error($"File must be smaller than {UploadConstants.MaxFileSizeInMegabytes} MB!");
            return false;
        }

        ImageErrorMessage = string.Empty;
        AppState.Storage.UploadIsInProgress = true;
        RefreshMediator.NotifyStateHasChangedSafe();

        AppState.Storage.IpfsImageUrl = await IpfsService.Upload(file.FileName, file.Type, file.ObjectURL);
        AppState.Storage.LocalImageUrl = new Uri(file.ObjectURL);
        ImageErrorMessage = string.Empty;
        AppState.Storage.UploadIsInProgress = false;
        AppState.Storage.CanPreviewTokenFile = file.IsPicture();

        if (!AppState.Storage.CanPreviewTokenFile)
        {
            MessageService.Warning("Can't preview this file. Tho you can still mint a NFT with it.");
        }

        RefreshMediator.NotifyStateHasChangedSafe();

        return false;
    }
}
