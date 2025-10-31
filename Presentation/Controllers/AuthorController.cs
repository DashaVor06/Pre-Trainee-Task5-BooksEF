using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTO;
using BusinessLogic.Services.AuthorService;

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
        public async Task<ActionResult<List<AuthorDTO>>> GetAsync()
        {
            return Ok(await _authorService.GetAllAsync());
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<BookAndAuthorDTO>> GetByIdAsync(int id)
        {
            AuthorDTO? res = await _authorService.GetByIdOrNullAsync(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<BookAndAuthorDTO>> GetByNameAsync(string name)
        {
            AuthorDTO? res = await _authorService.GetByNameOrNullAsync(name);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpGet("booksAmount")]
        public async Task<ActionResult<List<AuthorAndBooksAmountDTO>>> GetBooksAmountAsync()
        {
            return await _authorService.GetBooksAmountAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(AuthorDTO author)
        {
            AuthorDTO? res = await _authorService.CreateOrNullAsync(author);
            if (res != null)
                return Ok(author);
            else
                return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(AuthorDTO author)
        {
            AuthorDTO? res = await _authorService.UpdateOrNullAsync(author);
            if (res != null)
                return Ok(author);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _authorService.DeleteAsync(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
