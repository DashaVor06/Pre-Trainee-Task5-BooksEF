using AutoMapper;
using Business.DTO;
using Business.Validators;
using Data.Models;
using Data.Repositories;

namespace Business.Services
{
    public class AuthorService
    {
        private IRepository<Author> _reposAuthor = new AuthorRepository();
        private IRepository<Book> _reposBook = new BookRepository();
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> reposAuthor, IRepository<Book> reposBook, IMapper mapper)
        {
            _reposAuthor = reposAuthor;
            _reposBook = reposBook;
            _mapper = mapper;
        }

        public List<AuthorDTO> GetAll()
        {
            return _mapper.Map<List<AuthorDTO>>(_reposAuthor.GetAll());
        }
        
        public AuthorDTO? GetByIdOrNull (int id)
        {
            if (AuthorValidator.CheckIdExists(_reposAuthor.GetAll(), id))
            {
                return _mapper.Map<AuthorDTO>(_reposAuthor.GetById(id)); ;
            }
            return null;
        }
        
        public AuthorDTO? CreateOrNull(AuthorDTO author)
        {
            if (AuthorValidator.CheckAuthorForCreate(_reposAuthor.GetAll(), _mapper.Map<Author>(author)))
            {
                return _mapper.Map<AuthorDTO>(_reposAuthor.Create(_mapper.Map<Author>(author)));
            }
            return null;
        }

        public AuthorDTO? UpdateOrNull (AuthorDTO author)
        {
            if (AuthorValidator.CheckAuthorForUpdateDelete(_reposAuthor.GetAll(), _mapper.Map<Author>(author)))
            {
                return _mapper.Map<AuthorDTO>(_reposAuthor.Update(_mapper.Map<Author>(author)));
            }
            return null;
        }
        
        public bool Delete(int id)
        {
            Author? author = _reposAuthor.GetById(id);
            if (AuthorValidator.CheckAuthorForUpdateDelete(_reposAuthor.GetAll(), author))
            {
                _reposAuthor.Delete(id);
                
                foreach (Book book in _reposBook.GetAll().ToList())
                {
                    if (book.AuthorId == id)
                    {
                        _reposBook.Delete(book.Id);
                    }
                }

                return true;        
            }
            return false;
        }
    }
}
