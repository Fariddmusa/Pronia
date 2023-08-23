using Microsoft.AspNetCore.Http;
using Pronia.Core.Entities;

namespace Pronia.Business.Utilities;

public static class Extension
{
    public static bool CheckFileSize(this IFormFile file, int kb)
    {
        return file.Length / 1024 <= kb;
    }
    public static bool CheckFileType(this IFormFile file, string fileType)
    {
        return file.ContentType.Contains(fileType);
    }
    public static async Task<string> UploadFile(this IFormFile file, string root, string folderRoot)
    {
        string name = Guid.NewGuid().ToString() + file.FileName;
       
        string fileName = Path.Combine(folderRoot, name);
        string fileRoot = Path.Combine(root, fileName);
        using (FileStream fileStream = new FileStream(fileRoot, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            await file.CopyToAsync(fileStream);
        }
        return fileName;
    }
}
