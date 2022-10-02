using MimeTypes;
using NftFaucet.Components;
using NftFaucet.Domain.Models;
using Radzen;

namespace NftFaucet.Pages;

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
            MainFile = new TokenMedia
            {
                FileName = Model.MainFileName,
                FileType = DetermineFileType(Model.MainFileName),
                FileData = Model.MainFileData,
                FileSize = Model.MainFileSize!.Value,
            },
        };

        if (!string.IsNullOrEmpty(Model.CoverFileData) &&
            !string.IsNullOrEmpty(Model.CoverFileName) &&
            Model.CoverFileSize != null)
        {
            token.CoverFile = new TokenMedia
            {
                FileName = Model.CoverFileName,
                FileType = DetermineFileType(Model.CoverFileName),
                FileData = Model.CoverFileData,
                FileSize = Model.CoverFileSize!.Value,
            };
        }
        
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

        if (string.IsNullOrEmpty(Model.MainFileData))
            return false;

        if (string.IsNullOrEmpty(Model.MainFileName))
            return false;

        if (Model.MainFileSize is null or 0)
            return false;

        return true;
    }

    private void OnMainFileError(UploadErrorEventArgs args)
    {
        NotificationService.Notify(NotificationSeverity.Error, "File selection error", args.Message);
        Model.MainFileData = null;
        Model.MainFileName = null;
        Model.MainFileSize = null;
        Model.CoverFileData = null;
        Model.CoverFileName = null;
        Model.CoverFileSize = null;
    }

    private void OnCoverFileError(UploadErrorEventArgs args)
    {
        NotificationService.Notify(NotificationSeverity.Error, "File selection error", args.Message);
        Model.CoverFileData = null;
        Model.CoverFileName = null;
        Model.CoverFileSize = null;
    }

    private class NewFileModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainFileData { get; set; }
        public string MainFileName { get; set; }
        public long? MainFileSize { get; set; }
        public string CoverFileData { get; set; }
        public string CoverFileName { get; set; }
        public long? CoverFileSize { get; set; }
    }
}
