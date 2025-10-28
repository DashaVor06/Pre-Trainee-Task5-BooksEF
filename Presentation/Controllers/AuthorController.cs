using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using BusinessLogic.DTO;

namespace Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        AuthorService _authorService;
        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<AuthorDTO>> Get()
        {
            return Ok(_authorService.GetAll());
        }

        [HttpGet("id/{id}")]
        public ActionResult<BookAndAuthorDTO> GetById(int id)
        {
            AuthorDTO? res = _authorService.GetByIdOrNull(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpGet("name/{name}")]
        public ActionResult<BookAndAuthorDTO> GetByName(string name)
        {
            AuthorDTO? res = _authorService.GetByNameOrNull(name);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult Post(AuthorDTO author)
        {
            AuthorDTO? res = _authorService.CreateOrNull(author);
            if (res != null)
                return Ok(author);
            else
                return NotFound();
        }

        [HttpPut]
        public ActionResult Put(AuthorDTO author)
        {
            AuthorDTO? res = _authorService.UpdateOrNull(author);
            if (res != null)
                return Ok(author);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_authorService.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
