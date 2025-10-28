using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace DataAccess.DatabaseContext
{
    public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Лев Толстой", DateOfBirth = new DateOnly(1828, 9, 9) },
                new Author { Id = 2, Name = "Фёдор Достоевский", DateOfBirth = new DateOnly(1821, 11, 11) },
                new Author { Id = 3, Name = "Иван Тургенев", DateOfBirth = new DateOnly(1818, 11, 9) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Война и мир", PublishedYear = 1869, AuthorId = 1 },
                new Book { Id = 2, Title = "Анна Каренина", PublishedYear = 1877, AuthorId = 1 },
                new Book { Id = 3, Title = "Преступление и наказание", PublishedYear = 1866, AuthorId = 2 },
                new Book { Id = 4, Title = "Идиот", PublishedYear = 1869, AuthorId = 2 },
                new Book { Id = 5, Title = "Отцы и дети", PublishedYear = 1862, AuthorId = 3 },
                new Book { Id = 6, Title = "Записки охотника", PublishedYear = 1852, AuthorId = 3 }
            );
        }
    }
}
