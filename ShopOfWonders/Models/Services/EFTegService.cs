using SOW.DataModels;
using SOW.ShopOfWonders.Models.Interfaces;

namespace SOW.ShopOfWonders.Models.Services
{
    public class EFTegService : ITegService
    {
        public Task<Tag> CreateTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTagAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetTagByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
