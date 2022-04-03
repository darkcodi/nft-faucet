using Microsoft.AspNetCore.Components;

namespace NftFaucet.Extensions;

public static class NavigationManagerExtensions
{
    public static void NavigateToRelative(this NavigationManager uriHelper, string relativePath)
    {
        var newUri = uriHelper.BaseUri;
        if (!newUri.EndsWith("/"))
            newUri += "/";

        if (relativePath.StartsWith("/"))
            relativePath = relativePath.Substring(1);

        newUri += relativePath;

        uriHelper.NavigateTo(newUri);
    }
}
