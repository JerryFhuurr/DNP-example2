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
    public class AuthorController : ControllerBase
    {
        private AuthorDbContext dbContext;

        public AuthorController(AuthorDbContext authorDb) { dbContext = authorDb; }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<ActionResult<IList<Author>>> GetAuthors([FromQuery] int? id)
        {
            try
            {
                IList<Author> authors = await dbContext.Authors.ToListAsync();
                IList<Book> books = await dbContext.Books.ToListAsync();
                
                if (id != null)
                {
                    authors = authors.Where(a => a.Id == id).ToList();
                }

                return Ok(authors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }

        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor([FromBody] Author author)
        {
            try
            {
                EntityEntry<Author> newlyAdded = await dbContext.Authors.AddAsync(author);
                await dbContext.SaveChangesAsync();
                return Created($"/{author.Id}", author);
            }catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

    }
}
