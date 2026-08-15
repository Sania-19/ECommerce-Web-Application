using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using ShopVerseECommercePlatform.Application.RRModels.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IStorageService
{
    public interface IStorageService
    {
        Task<(string,string)> SaveFileAsync(IFormFile file);

        Task <(IEnumerable<FileResponse>,int)> SaveFilesAsync(IFormFileCollection files);

        Task<IEnumerable<string>> SaveFilesAsync(List<IFormFile> files);

        Task<(string,string)> UpdateFileAsync(IFormFile file, string existingFileName);

        void DeleteFileAsync(string fileName);

        int DeleteFilesAsync(IEnumerable<string> fileNames);
    }
}
