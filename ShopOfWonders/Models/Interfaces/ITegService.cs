using SOW.DataModels;

namespace SOW.ShopOfWonders.Models.Interfaces
{
    public interface ITegService
    {
        public Task<IEnumerable<Tag>> GetAllTagsAsync();

        public Task<Tag> GetTagByIdAsync(long id);

        public Task<Tag> CreateTagAsync(Tag tag);

        public Task UpdateTagAsync(Tag tag);

        public Task DeleteTagAsync(long id);
    }
}
