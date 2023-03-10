using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models.Interfaces;
using System.IO;

namespace SOW.ShopOfWonders.Models.Services
{
    public class FileService : IFileService
    {
        private IdentityContext _context;

        private IAntyVirusService _antyVirusService;

        private IWebHostEnvironment _webHostEnvironment;

        public FileService(IdentityContext context , IAntyVirusService antyVirusService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _antyVirusService = antyVirusService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteFile(string fileName)
        {
            if(File.Exists("/Files/" + fileName))
            {
                File.Delete("/Files/" + fileName);

                FileModel fileModel = await _context.FileModels.FirstOrDefaultAsync(f => f.Name == fileName);

                _context.FileModels.Remove(fileModel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FileModel>> GetAllFiles()
        {
            return await _context.FileModels.ToListAsync();
        }

        public Task<IQueryable<FileModel>> GetAllFilesWithFilter()
        {
            return Task.FromResult(_context.FileModels.AsQueryable());
        }

        public async Task UploadFile(IFormFile formFile, User user)
        {
            if (await _antyVirusService.IsVirus(formFile, new CancellationToken()))
            {
                throw new Exception("Файл помечен как вирус!"); // нужно сделать своии исключения
            }
#if DEBUG
            if (user is null)
            {
                user = await _context.Users.FirstOrDefaultAsync();
            }
#endif
            string path = "/Files/" + formFile.FileName;

            using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            FileModel fileModel = new()
            {
                Name = formFile.Name,
                Description = formFile.ContentDisposition.ToString(),
                Size = formFile.Length,
                Author = user,
                Path = path
            };

            await _context.FileModels.AddAsync(fileModel);
            await _context.SaveChangesAsync();
        }

        public async Task UploadFile(IBrowserFile formFile, User user)
        {
            /// выаолнить проверку антивируса
#if DEBUG
            if (user is null)
            {
                user = await _context.Users.FirstOrDefaultAsync();
            }
#endif
            string path = _webHostEnvironment.WebRootPath + @"\Files\" + formFile.Name;

            //using (FileStream fs = new(path, FileMode.Create))
            //{
            //    await formFile.OpenReadStream().CopyToAsync(fs);
            //}

            FileStream fs = null;
            try
            {
                fs = new(path, FileMode.Create);
                await formFile.OpenReadStream().CopyToAsync(fs);
            }
            catch
            {
                throw new Exception("Ошибка созранения файла!");
            }
            finally
            {
                fs.Dispose();
            }

            if (await _antyVirusService.IsVirus(path, new CancellationToken()))
            {
                throw new Exception("Файл помечен как вирус!"); // нужно сделать своии исключения
            }


            FileModel fileModel = new()
            {
                Name = formFile.Name,
                Description = formFile.ContentType,
                Size = formFile.Size,
                Author = user,
                Path = path
            };

            await _context.FileModels.AddAsync(fileModel);
            await _context.SaveChangesAsync();
        }
    }
}
