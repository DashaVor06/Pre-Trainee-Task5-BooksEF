﻿using BusinessLogic.Services;
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
        public ActionResult<List<BookDTO>> Get()
        {
            return Ok(_bookService.GetAllBooks());
        }

        [HttpGet("id/{id}")]
        public ActionResult<BookAndAuthorDTO> GetById(int id)
        {
            BookAndAuthorDTO? res = _bookService.GetByIdOrNull(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpGet("publishedAfter/{date}")]
        public ActionResult<List<BookAndAuthorDTO>> GetPublishedAfter(int date)
        {
            return Ok(_bookService.GetPublishedAfter(date));
        }

        [HttpPost]
        public ActionResult Post(BookDTO book)
        {
            BookDTO? res = _bookService.CreateOrNull(book);
            if (res != null)
                return Ok(res);
            else
                return NotFound();         
        }

        [HttpPut]
        public ActionResult Put(BookDTO book)
        {
            BookDTO? res = _bookService.UpdateOrNull(book);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_bookService.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
