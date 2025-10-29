using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Validators;
using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class AuthorService
    {
        private AuthorRepository _reposAuthor;
        private BookRepository _reposBook;
        private readonly IMapper _mapper;
        public AuthorService(AuthorRepository reposAuthor, BookRepository reposBook, IMapper mapper)
        {
            _reposAuthor = reposAuthor;
            _reposBook = reposBook;
            _mapper = mapper;
        }
        public async Task<List<AuthorDTO>> GetAllAsync()
        {
            return _mapper.Map<List<AuthorDTO>>(await _reposAuthor.GetAllAsync());
        }       
        public async Task<AuthorDTO?> GetByIdOrNullAsync(int id)
        {
            if (AuthorValidator.CheckIdExists(await _reposAuthor.GetAllAsync(), id))
            {
                return _mapper.Map<AuthorDTO>(await _reposAuthor.GetByIdAsync(id)); ;
            }
            return null;
        }
        public async Task<AuthorDTO?> GetByNameOrNullAsync(string name)
        {
            if (AuthorValidator.CheckNameExists(await _reposAuthor.GetAllAsync(), name))
            {
                return _mapper.Map<AuthorDTO>(await _reposAuthor.GetByNameAsync(name)); ;
            }
            return null;
        }
        public async Task<List<AuthorAndBooksAmountDTO>> GetBooksAmountAsync()
        {
            var listAuthors = await _reposAuthor.GetAllAsync();
            var booksCountByAuthor = await _reposBook.GetBooksAmountAsync();

            var result = (from author in listAuthors
                          join bookCount in booksCountByAuthor on author.Id equals bookCount.Key into authorBooks
                          from bookCount in authorBooks.DefaultIfEmpty()
                          select new AuthorAndBooksAmountDTO
                          {
                              Id = author.Id,
                              Name = author.Name,
                              DateOfBirth = author.DateOfBirth,
                              BooksAmount = bookCount.Value // Будет 0 если книг нет
                          }).ToList();

            return result;
        }
        public async Task<AuthorDTO?> CreateOrNullAsync(AuthorDTO author)
        {
            if (AuthorValidator.CheckAuthorForCreate(await _reposAuthor.GetAllAsync(), _mapper.Map<Author>(author)))
            {
                return _mapper.Map<AuthorDTO>(await _reposAuthor.CreateAsync(_mapper.Map<Author>(author)));
            }
            return null;
        }
        public async Task<AuthorDTO?> UpdateOrNullAsync(AuthorDTO author)
        {
            if (AuthorValidator.CheckAuthorForUpdateDelete(await _reposAuthor.GetAllAsync(), _mapper.Map<Author>(author)))
            {
                return _mapper.Map<AuthorDTO>(await _reposAuthor.UpdateAsync(_mapper.Map<Author>(author)));
            }
            return null;
        }       
        public async Task<bool> DeleteAsync(int id)
        {
            Author? author = await _reposAuthor.GetByIdAsync(id);
            if (AuthorValidator.CheckAuthorForUpdateDelete(await _reposAuthor.GetAllAsync(), author))
            { 
                var allBooks = await _reposBook.GetAllAsync();
                foreach (Book book in allBooks)
                {
                    if (book.AuthorId == id)
                    {
                        await _reposBook.DeleteAsync(book.Id);
                    }
                }

                return true;        
            }
            return false;
        }
    }
}
