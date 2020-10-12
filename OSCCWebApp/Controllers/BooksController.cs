using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {

        private readonly OSCC_NEWContext _context;

        public BooksController(OSCC_NEWContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByAuthorID([FromQuery] int authorID)
        {
            return Json(_context.Books.ToList().Where(i => i.Author == authorID));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Books book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    return Ok(book);
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(book);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteByTitle([FromBody] Books book)
        {
            Books _book = _context.Books.ToList().Where(i => i.Id == book.Id).FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Books.Remove(_book);
                    _context.SaveChanges();
                    return Ok(_book);
                }

            }
            catch (Exception ex)
            {
                // log
            }
            return NotFound(book);
        }
    }
}