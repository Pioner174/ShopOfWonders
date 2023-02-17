using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models.Interfaces;

namespace SOW.ShopOfWonders.Models.Services
{
    public class EFTegService : ITegService
    {
        private IdentityContext _context;

        public EFTegService(IdentityContext context)
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
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return tag;
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
    }
}
