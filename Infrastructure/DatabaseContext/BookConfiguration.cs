using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DatabaseContext
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books").HasKey(p => p.Id);
            builder.HasOne(b => b.Author)       
               .WithMany(a => a.Books)       
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
