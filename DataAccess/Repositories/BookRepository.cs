using DataAccess.DatabaseContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly DbContextOptions<LibraryContext> _options;
        public BookRepository(DbContextOptions<LibraryContext> options)
        {
            _options = options;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            using (var context = new LibraryContext(_options))
            {
                return await context.Books.ToListAsync();
            }
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Book res = await (from book in context.Books
                              where book.Id == id
                              select book).FirstOrDefaultAsync();
                return res;
            }
        }
        public async Task<List<Book>> GetPublishedAfterAsync(int year)
        {
            using (var context = new LibraryContext(_options))
            {
                List<Book> res = await (from book in context.Books
                                 where book.PublishedYear > year
                                 select book).ToListAsync();
                return res;
            }
        }
        public async Task<Book> CreateAsync(Book book)
        {
            using (var context = new LibraryContext(_options))
            {
                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();
                return book;
            }
        }
        public async Task<Book> UpdateAsync(Book book)
        {
            using (var context = new LibraryContext(_options))
            {
                Book res = await (from bookTemp in context.Books
                              where bookTemp.Id == book.Id
                              select bookTemp).FirstOrDefaultAsync();

                res.Title = book.Title;
                res.AuthorId = book.AuthorId;
                res.PublishedYear = book.PublishedYear;

                await context.SaveChangesAsync();
                return res;
            }
        }
        public async Task DeleteAsync(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Book res = await (from book in context.Books
                              where book.Id == id
                              select book).FirstOrDefaultAsync();
                context.Books.Remove(res);
                await context.SaveChangesAsync();
            }
        }
    }
}
