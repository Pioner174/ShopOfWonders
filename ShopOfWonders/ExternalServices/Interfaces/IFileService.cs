using Microsoft.AspNetCore.Components.Forms;
using SOW.DataModels;

namespace SOW.ShopOfWonders.ExternalServices.Interfaces
{
    public interface IFileService
    {

        public Task<IEnumerable<FileModel>> GetAllFiles();

        public Task<IQueryable<FileModel>> GetAllFilesWithFilter();

        public Task UploadFile(IFormFile formFile, User user);

        public Task UploadFile(IBrowserFile formFile, User user);

        public Task DeleteFile(string fileName);

    }
}
