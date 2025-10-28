using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Validators;
using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class BookService
    {
        private BookRepository _reposBook;
        private AuthorRepository _reposAuthor;
        private readonly IMapper _mapper;
        public BookService(AuthorRepository reposAuthor, BookRepository reposBook, IMapper mapper)
        {
            _reposAuthor = reposAuthor;
            _reposBook = reposBook;
            _mapper = mapper;
        }
        private BookAndAuthorDTO GetBookAndAuthorClass(Book book, Author author)
        {
            BookAndAuthorDTO bookAndAuthor = new BookAndAuthorDTO();
            bookAndAuthor.Id = book.Id;
            bookAndAuthor.Title = book.Title;
            bookAndAuthor.PublishedYear = book.PublishedYear;
            bookAndAuthor.AuthorId = book.AuthorId;
            bookAndAuthor.Name = author.Name;
            bookAndAuthor.DateOfBirth = author.DateOfBirth;
            return bookAndAuthor;      
        }
        private async Task<List<BookAndAuthorDTO>> GetListOfBooksAndAuthors(List<Book> listBooks)
        {
            var listBookAndAuthor = new List<BookAndAuthorDTO>();
            var listAuthors = await _reposAuthor.GetAllAsync();

            foreach (Book book in listBooks)
            {
                Author? author = (from authorTemp in listAuthors
                                  where authorTemp.Id == book.AuthorId
                                  select authorTemp).FirstOrDefault();

                if (author != null)
                {
                    listBookAndAuthor.Add(GetBookAndAuthorClass(book, author));
                }

            }
            return listBookAndAuthor;
        }
        public async Task<List<BookAndAuthorDTO>> GetAllBooksAsync()
        {
            List<Book> listBooks = await _reposBook.GetAllAsync();
            return await GetListOfBooksAndAuthors(listBooks);
        }
        public async Task<BookAndAuthorDTO?> GetByIdOrNullAsync(int id)
        {
            if (BookValidator.CheckIdExists(await _reposBook.GetAllAsync(), id))
            {
                Book book = await _reposBook.GetByIdAsync(id);
                if (AuthorValidator.CheckIdExists(await _reposAuthor.GetAllAsync(), book.AuthorId))
                {
                    Author author = await _reposAuthor.GetByIdAsync(book.AuthorId);
                    return GetBookAndAuthorClass(book, author);
                }
            }
            return null;
        }
        public async Task<List<BookAndAuthorDTO>> GetPublishedAfterAsync(int year)
        {
            List<Book> listBooks = await _reposBook.GetPublishedAfterAsync(year);
            return await GetListOfBooksAndAuthors(listBooks);
        }
        public async Task<BookDTO?> CreateOrNullAsync(BookDTO book)
        {
            if (BookValidator.CheckBookForCreate(await _reposBook.GetAllAsync(), await _reposAuthor.GetAllAsync(), _mapper.Map<Book>(book)))
            {
                return _mapper.Map<BookDTO>(await _reposBook.CreateAsync(_mapper.Map<Book>(book)));
            }
            return null;    
        }
        public async Task<BookDTO?> UpdateOrNullAsync(BookDTO book)
        {
            if (BookValidator.CheckBookForUpdateDelete(await _reposBook.GetAllAsync(), await _reposAuthor.GetAllAsync(), _mapper.Map<Book>(book)))
            {
                return _mapper.Map<BookDTO>(await _reposBook.UpdateAsync(_mapper.Map<Book>(book)));
            }
            return null;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Book? book = await _reposBook.GetByIdAsync(id);
            if (BookValidator.CheckBookForUpdateDelete(await _reposBook.GetAllAsync(), await _reposAuthor.GetAllAsync(), book))
            {
                await _reposBook.DeleteAsync(id);
                return true;
            }
            return false;
        }
    }
}
