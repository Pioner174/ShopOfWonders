using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models.Interfaces;

namespace SOW.ShopOfWonders.Models.Services
{
    public class EFTagService : ITagService
    {
        private IdentityContext _context;

        public EFTagService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByIdAsync(long id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> CreateTagAsync(Tag tag)
        {
            if(await IsTagAvailable(tag))
            {
                _context.Tags.Add(tag);

                await _context.SaveChangesAsync();
            }

            return await _context.Tags.FirstOrDefaultAsync(t=> t.Name == tag.Name);
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(long id)
        {
            var tag = await _context.Tags.FindAsync(id);
            tag.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTagAvailable(Tag tag)
        {
            if(await _context.Tags.FirstOrDefaultAsync(t=>t.Name== tag.Name) == null)
            {
                return true;
            }
            return false;
        }
    }
}
