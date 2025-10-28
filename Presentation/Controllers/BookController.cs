using BusinessLogic.Services;
using BusinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAsync()
        {
            return Ok(await _bookService.GetAllBooksAsync());
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<BookAndAuthorDTO>> GetByIdAsync(int id)
        {
            BookAndAuthorDTO? res = await _bookService.GetByIdOrNullAsync(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpGet("publishedAfter/{date}")]
        public async Task<ActionResult<List<BookAndAuthorDTO>>> GetPublishedAfterAsync(int date)
        {
            return Ok(await _bookService.GetPublishedAfterAsync(date));
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(BookDTO book)
        {
            BookDTO? res = await _bookService.CreateOrNullAsync(book);
            if (res != null)
                return Ok(res);
            else
                return NotFound();         
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(BookDTO book)
        {
            BookDTO? res = await _bookService.UpdateOrNullAsync(book);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _bookService.DeleteAsync(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
