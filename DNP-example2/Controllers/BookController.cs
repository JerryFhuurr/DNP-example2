using DNP_example2.Appropriately;
using DNP_example2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DNP_example2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private AuthorDbContext _authorDbContext;
        public BookController(AuthorDbContext dbContext) { _authorDbContext = dbContext; }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IList<Book>>> GetBooks([FromQuery] int? Isbn)
        {
            try
            {
                IList<Book> books = await _authorDbContext.Books.ToListAsync();
                IList<Author> authors = await _authorDbContext.Authors.ToListAsync();
                if (Isbn != null)
                {
                    books = books.Where(b => b.ISBN == Isbn).ToList();
                }

                return Ok(books);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }

        }

        // POST api/<BookController>
        [HttpPost("/[controller]")]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book, [FromQuery]int id)
        {
            try
            {
                IList<Author> authors = await _authorDbContext.Authors.ToListAsync();
                if (id != null)
                {
                    authors = authors.Where(a => a.Id == id).ToList();
                }
                foreach (Author author in authors)
                {
                    if (author.Id == id)
                    {
                        author.Books.Add(book);
                    }
                }

                EntityEntry<Book> newlyAdded = await _authorDbContext.Books.AddAsync(book);
                await _authorDbContext.SaveChangesAsync();
                return Created($"/{book.Title}", book);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{ISBN}")]
        public async Task<ActionResult> DeleteBook([FromRoute] int ISBN)
        {
            try
            {
                Book bookToRemove = await _authorDbContext.Books.FirstOrDefaultAsync(b => b.ISBN == ISBN);
                if (bookToRemove != null)
                {
                    _authorDbContext.Books.Remove(bookToRemove);
                    await _authorDbContext.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }

        }
    }
}
