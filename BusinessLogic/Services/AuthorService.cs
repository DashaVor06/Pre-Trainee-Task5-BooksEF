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
        public AuthorDTO? GetByNameOrNull(string name)
        {
            if (AuthorValidator.CheckNameExists(_reposAuthor.GetAll(), name))
            {
                return _mapper.Map<AuthorDTO>(_reposAuthor.GetByName(name)); ;
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
