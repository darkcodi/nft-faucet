using MimeTypes;
using NftFaucetRadzen.Components;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Plugins;

namespace NftFaucetRadzen.Pages;

public partial class CreateTokenDialog : BasicComponent
{
    private NewFileModel Model { get; set; } = new NewFileModel();
    private bool ModelIsValid => IsValid();

    private void OnSavePressed()
    {
        if (!IsValid())
            return;

        var token = new Token
        {
            Id = Guid.NewGuid(),
            Name = Model.Name,
            Description = Model.Description,
            CreatedAt = DateTime.Now,
            Image = new TokenMedia
            {
                FileName = Model.FileName,
                FileType = DetermineFileType(Model.FileName),
                FileData = Model.FileData,
                FileSize = Model.FileSize!.Value,
            },
        };
        DialogService.Close(token);
    }

    private string DetermineFileType(string fileName)
    {
        var extension = fileName.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();
        if (!MimeTypeMap.TryGetMimeType(extension, out var mimeType))
        {
            return "application/octet-stream";
        }

        return mimeType;
    }

    private bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Model.Name))
            return false;

        if (string.IsNullOrWhiteSpace(Model.Description))
            return false;

        if (string.IsNullOrEmpty(Model.FileData))
            return false;

        if (string.IsNullOrEmpty(Model.FileName))
            return false;

        if (Model.FileSize is null or 0)
            return false;

        return true;
    }
}
