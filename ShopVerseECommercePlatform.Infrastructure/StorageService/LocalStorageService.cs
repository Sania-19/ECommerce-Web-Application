
using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.Abstraction.IStorageService;
using ShopVerseECommercePlatform.Application.RRModels.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Infrastructure.StorageService
{
    public class LocalStorageService(string webRootPath) : IStorageService
    {
        #region HELPERS
        private string GetPhysicalPath => Path.Combine(webRootPath, "Files");
        private string GetVirtualPath(string FileName) => "/Files/" + FileName;
        #endregion

        #region SAVE
        public async Task<(string, string)> SaveFileAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var newFileName = string.Concat(Guid.CreateVersion7().ToString() + extension);

            if (!Directory.Exists(GetPhysicalPath))
            {
                Directory.CreateDirectory(GetPhysicalPath);
            }

            var absolutePath = Path.Combine(GetPhysicalPath, newFileName);
            FileStream fileStream = new FileStream(absolutePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            var virtualPath = GetVirtualPath(newFileName);
            return (virtualPath, newFileName);
        }

        public async Task<(IEnumerable<FileResponse>, int)> SaveFilesAsync(IFormFileCollection files)
        {
            int totalFilesUploaded = 0;
            List<FileResponse> fileResponse = new List<FileResponse>();
            foreach (var file in files)
            {
                (string filePath, string fileName) = await SaveFileAsync(file);
                fileResponse.Add(new FileResponse
                {
                    FileName = fileName,
                    FilePath = filePath,
                });
            }
            return (fileResponse, totalFilesUploaded);
        }

        public async Task<IEnumerable<string>> SaveFilesAsync(List<IFormFile> files)
        {
            throw new NotImplementedException();

        }
        #endregion

        #region DELETE
        public void DeleteFileAsync(string fileName)
        {
            string filePath = Path.Combine(GetPhysicalPath, fileName);
            File.Delete(filePath);
        }

        public int DeleteFilesAsync(IEnumerable<string> fileNames)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region UPDATE
        public async Task<(string, string)> UpdateFileAsync(IFormFile file, string existingFileName)
        {
            string filePath = Path.Combine(GetPhysicalPath, existingFileName);
            if (file is not null)
            {
                File.Delete(filePath);
                return await SaveFileAsync(file);
            }
            var filePath2 = GetVirtualPath(existingFileName);
            return (filePath2, existingFileName);
        }
        #endregion
    }
}
