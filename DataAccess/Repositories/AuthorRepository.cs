using Microsoft.EntityFrameworkCore;
using DataAccess.DatabaseContext;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly DbContextOptions<LibraryContext> _options;

        public AuthorRepository(DbContextOptions<LibraryContext> options)
        {
            _options = options;
        }

        public List<Author> GetAll()
        {
            using (var context = new LibraryContext(_options))
            {
                return context.Authors.ToList();
            }
        }

        public Author GetById(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = (from author in context.Authors
                             where author.Id == id
                             select author).FirstOrDefault();
                return res;
            }        
        }

        public Author GetByName(string name)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = (from author in context.Authors
                              where author.Name.Contains(name)
                              select author).FirstOrDefault();
                return res;
            }
        }

        public Author Create(Author author)
        {
            using (var context = new LibraryContext(_options))
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return author;
            }
        }
        public Author Update(Author author)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = (from authorTemp in context.Authors
                              where authorTemp.Id == author.Id
                              select authorTemp).FirstOrDefault();

                res.Name = author.Name;
                res.DateOfBirth = author.DateOfBirth;

                context.SaveChanges();
                return res;
            }
        }
        public void Delete(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = (from author in context.Authors
                              where author.Id == id
                              select author).FirstOrDefault();
                context.Authors.Remove(res);
                context.SaveChanges();
            }
        }
    }
}
