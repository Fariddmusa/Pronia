using Microsoft.AspNetCore.Http;
using Pronia.Business.Exceptions;
using Pronia.Business.Services.Interfaces;
using Pronia.Business.Utilities;
using Pronia.Core.Entities;

namespace Pronia.Business.Services.Implementations;

public class FileService : IFileService
{
    public void RemoveFile(string root, string filePath)
    {
        string fileRoot = Path.Combine(root, filePath);
        if (File.Exists(fileRoot))
        {
            File.Delete(fileRoot);
        }
    }

    public async Task<string> UploadFile(IFormFile file, string root, params string[] folders)
    {
        if (!file.CheckFileSize(300))
        {
            throw new FileSizeException("File size must be less than 300kb");
        }

        if (!file.CheckFileType("image"))
        {
            throw new FileTypeException("Please select image type");
        }
        string folderRoot = string.Empty;
        foreach (var folder in folders)
        {
            folderRoot = Path.Combine(folderRoot, folder);
        }
        string fileName = await file.UploadFile(root, folderRoot);
        return fileName;
    }
}
