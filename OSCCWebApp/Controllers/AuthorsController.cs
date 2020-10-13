using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSCCWebApp;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly OSCC_DBContext _context;

        public AuthorsController(OSCC_DBContext context)
        {
            _context = context;
        }

        // GET: Authors
        [HttpGet]
        public IActionResult Index()
        {
            return Json(_context.Authors.ToList());
        }


        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([FromBody] Authors author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Authors.Add(author);
                    
                    _context.SaveChanges();
                    return Ok(author);
                }

            } catch (Exception ex)
            {
                // log
            }

            return Conflict(author);
        }

        
        // POST: Authors/Delete/
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteByName([FromBody] Authors author)
        {
            Authors _author = _context.Authors.ToList().Where(i => i.Id == author.Id).FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Authors.Remove(_author);
                    _context.SaveChanges();
                    return Ok(_author);
                }

            } catch (Exception ex)
            {
                // log
                // return StatusCode(418);;

            }
            return NotFound();
        }

        private bool AuthorsExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
