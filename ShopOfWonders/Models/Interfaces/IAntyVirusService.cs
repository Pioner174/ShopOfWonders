namespace SOW.ShopOfWonders.Models.Interfaces
{
    public interface IAntyVirusService
    {
        public Task<bool> IsVirus(string filePath, CancellationToken cancellationToken);

        public Task<bool> IsVirus(IFormFile file, CancellationToken cancellationToken);
    }
}
