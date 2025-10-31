using Microsoft.EntityFrameworkCore;
using Infrastructure.DatabaseContext;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly DbContextOptions<LibraryContext> _options;

        public AuthorRepository(DbContextOptions<LibraryContext> options)
        {
            _options = options;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            using (var context = new LibraryContext(_options))
            {
                return await context.Authors.ToListAsync();
            }
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = await (from author in context.Authors
                                    where author.Id == id
                                    select author).FirstOrDefaultAsync();
                return res;
            }
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = await (from author in context.Authors
                                    where author.Name.Contains(name)
                                    select author).FirstOrDefaultAsync();
                return res;
            }
        }

        public async Task<Author> CreateAsync(Author author)
        {
            using (var context = new LibraryContext(_options))
            {
                author.Id = 0;
                await context.Authors.AddAsync(author);
                await context.SaveChangesAsync();
                return author;
            }
        }
        public async Task<Author> UpdateAsync(Author author)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = await (from authorTemp in context.Authors
                                    where authorTemp.Id == author.Id
                                    select authorTemp).FirstOrDefaultAsync();

                res.Name = author.Name;
                res.DateOfBirth = author.DateOfBirth;

                await context.SaveChangesAsync();
                return res;
            }
        }
        public async Task DeleteAsync(int id)
        {
            using (var context = new LibraryContext(_options))
            {
                Author res = await (from author in context.Authors
                                    where author.Id == id
                                    select author).FirstOrDefaultAsync();
                context.Authors.Remove(res);
                await context.SaveChangesAsync();
            }
        }
    }
}
