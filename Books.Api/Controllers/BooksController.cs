using Books.Api.Models;
using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Books.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;

        public BooksController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet(Name = "get")]
        public ActionResult<List<Book>> Get() =>
            bookService.Get();

        [HttpGet("{id:length(24)}", Name = "get-book")]
        public ActionResult<Book> Get(string id)
        {
            var book = bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost(Name = "post")]
        public ActionResult<Book> Post(Book book)
        {
            bookService.Create(book);
            return CreatedAtRoute("get-book", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}", Name = "put")]
        public IActionResult Put(string id, Book book)
        {
            var b = bookService.Get(id);
            if (b == null)
            {
                return NotFound();
            }
            bookService.Update(id, book);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}", Name = "delete")]
        public IActionResult Delete(string id)
        {
            var book = bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            bookService.Remove(book.Id);
            return NoContent();
        }
    }
}
