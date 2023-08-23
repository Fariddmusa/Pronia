using Microsoft.AspNetCore.Http;

namespace Pronia.Business.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string root, params string[] folders);
    void RemoveFile(string root, string filePath);
}
