using SOW.DataModels;

namespace SOW.ShopOfWonders.ExternalServices.Interfaces
{
    public interface ITagService
    {
        public Task<IEnumerable<Tag>> GetAllTagsAsync();

        public Task<Tag> GetTagByIdAsync(long id);

        public Task<Tag> CreateTagAsync(Tag tag);

        public Task UpdateTagAsync(Tag tag);

        public Task DeleteTagAsync(long id);

        public Task<bool> IsTagAvailable(Tag tag);
    }
}
