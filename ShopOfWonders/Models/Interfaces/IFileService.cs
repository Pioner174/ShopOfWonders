using SOW.DataModels;

namespace SOW.ShopOfWonders.Models.Interfaces
{
    public interface IFileService
    {

        public Task<IEnumerable<FileModel>> GetAllFiles();

        public Task<IQueryable<FileModel>> GetAllFilesWithFilter();

        public Task UploadFile(IFormFile formFile, User user);

        public Task DeleteFile(string fileName);

    }
}
