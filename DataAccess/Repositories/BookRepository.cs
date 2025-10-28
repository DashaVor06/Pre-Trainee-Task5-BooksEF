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
        public List<Book> GetAll()
        {
            using (var context = new LibraryContext(_options))
            {
                return context.Books.ToList();
            }
        }
        public Book GetById(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Book res = (from book in context.Books
                              where book.Id == id
                              select book).FirstOrDefault();
                return res;
            }
        }
        public List<Book> GetPublishedAfter(int year)
        {
            using (var context = new LibraryContext(_options))
            {
                List<Book> res = (from book in context.Books
                                 where book.PublishedYear > year
                                 select book).ToList();
                return res;
            }
        }
        public Book Create(Book book)
        {
            using (var context = new LibraryContext(_options))
            {
                context.Books.Add(book);
                context.SaveChanges();
                return book;
            }
        }
        public Book Update(Book book)
        {
            using (var context = new LibraryContext(_options))
            {
                Book res = (from bookTemp in context.Books
                              where bookTemp.Id == book.Id
                              select bookTemp).FirstOrDefault();

                res.Title = book.Title;
                res.AuthorId = book.AuthorId;
                res.PublishedYear = book.PublishedYear;

                context.SaveChanges();
                return res;
            }
        }
        public void Delete(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Book res = (from book in context.Books
                              where book.Id == id
                              select book).FirstOrDefault();
                context.Books.Remove(res);
                context.SaveChanges();
            }
        }
    }
}
