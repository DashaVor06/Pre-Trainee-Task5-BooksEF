﻿using AutoMapper;
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
        private List<BookAndAuthorDTO> GetListOfBooksAndAuthors(List<Book> listBooks)
        {
            var listBookAndAuthor = new List<BookAndAuthorDTO>();
            Author? author = null;

            foreach (Book book in listBooks)
            {
                author = _reposAuthor.GetById(book.AuthorId);
                if (author != null)
                {
                    listBookAndAuthor.Add(GetBookAndAuthorClass(book, author));
                }

            }
            return listBookAndAuthor;
        }
        public List<BookAndAuthorDTO> GetAllBooks()
        {
            List<Book> listBooks = _reposBook.GetAll();
            return GetListOfBooksAndAuthors(listBooks);
        }
        public BookAndAuthorDTO? GetByIdOrNull(int id)
        {
            if (BookValidator.CheckIdExists(_reposBook.GetAll(), id))
            {
                Book book = _reposBook.GetById(id);
                if (AuthorValidator.CheckIdExists(_reposAuthor.GetAll(), book.AuthorId))
                {
                    Author author = _reposAuthor.GetById(book.AuthorId);
                    return GetBookAndAuthorClass(book, author);
                }
            }
            return null;
        }
        public List<BookAndAuthorDTO> GetPublishedAfter(int year)
        {
            List<Book> listBooks = _reposBook.GetPublishedAfter(year);
            return GetListOfBooksAndAuthors(listBooks);
        }
        public BookDTO? CreateOrNull(BookDTO book)
        {
            if (BookValidator.CheckBookForCreate(_reposBook.GetAll(), _reposAuthor.GetAll(), _mapper.Map<Book>(book)))
            {
                return _mapper.Map<BookDTO>(_reposBook.Create(_mapper.Map<Book>(book)));
            }
            return null;    
        }
        public BookDTO? UpdateOrNull(BookDTO book)
        {
            if (BookValidator.CheckBookForUpdateDelete(_reposBook.GetAll(), _reposAuthor.GetAll(), _mapper.Map<Book>(book)))
            {
                return _mapper.Map<BookDTO>(_reposBook.Update(_mapper.Map<Book>(book)));
            }
            return null;
        }
        public bool Delete(int id)
        {
            Book? book = _reposBook.GetById(id);
            if (BookValidator.CheckBookForUpdateDelete(_reposBook.GetAll(), _reposAuthor.GetAll(), book))
            {
                _reposBook.Delete(id);
                return true;
            }
            return false;
        }
    }
}
