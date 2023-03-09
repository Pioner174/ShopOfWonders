using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOW.DataModels;

namespace SOW.ShopOfWonders.Models
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ProductsTags> ProductsTags { get; set; }

        public DbSet<FileModel> FileModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<User>(u =>
            {
                u.HasKey(t => t.Id);

                u.HasIndex(u => u.UserName).IsUnique();

                // Один ко многим
                u.HasMany(a => a.Comments)
                .WithOne(c => c.Author);
            });

            builder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);

                p.HasIndex(p => p.Name);
                // Один к одному
                p.HasOne(p => p.Category);

                // Один ко многим
                p.HasMany(p => p.Comments)
                .WithOne(c => c.Product);

                // Многие ко многим
                p.HasMany(p => p.Tags)
                .WithMany(t => t.Products)
                .UsingEntity<ProductsTags>(
                    tag => tag.HasOne(pt => pt.Tag)
                    .WithMany(tag => tag.ProductsTags)
                    .HasForeignKey(pt => pt.TagId),
                    product => product.HasOne(pa => pa.Product)
                    .WithMany(tag => tag.ProductsTags)
                    .HasForeignKey(pt => pt.ProductId));

            });

            builder.Entity<Category>(c =>
            {
                c.HasKey(t => t.CategoryId);

                c.HasIndex(t => t.Name).IsUnique();
                // Один ко многим
                c.HasMany(co => co.Products)
                .WithOne(c => c.Category);
            });

            builder.Entity<Comment>(com =>
            {
                com.HasKey(c => c.CommentId);
                // Один к одному
                com.HasOne(c => c.Product);
                // Один к одному
                com.HasOne(c => c.Author);
            });

            builder.Entity<Tag>(tag =>
            {
                tag.HasKey(c => c.TagId);

                tag.HasIndex(c => c.Name).IsUnique();

            });

            //Работа с фалами 
            builder.Entity<FileModel>(file =>
            {
                file.HasKey(f=> f.Id);

                file.HasOne(f => f.Author);
            });

            base.OnModelCreating(builder);
        }


    }
}
