using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BibliographyController : Controller
    {
        private readonly OSCC_DEVContext _context;

        public BibliographyController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByBook([FromQuery] string title)
        {
            return Json(_context.Bibliography.ToList().Where(i => i.Book == title));
        }
    }
}