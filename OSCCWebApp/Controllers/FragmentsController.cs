using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FragmentsController : Controller
    {
        private readonly OSCC_DEVContext _context;

        public FragmentsController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult FromBookID([FromQuery] int bookID)
        {
            return Json(_context.Fragments.ToList().Where(i => i.Book == bookID));
        }

        [HttpGet]
        [Route("Hello")]
        public IActionResult GetReferencerID2([FromQuery] string fragmentID, [FromQuery] int editorID, [FromQuery] int bookID)
        {
            return Json(_context.Fragments.ToList().Where(i => i.FragmentName == fragmentID).Where(i => i.Editor == editorID).Where(i => i.Book == bookID).Select(x => x.Id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Fragments fragment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Fragments.Add(fragment);
                    _context.SaveChanges();
                    return Ok(fragment);
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(fragment);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteByAttr([FromBody] Fragments fragment)
        {
            Fragments _fragment = _context.Fragments.ToList().Where(i => i.FragmentName == fragment.FragmentName)
                                                             .Where(i => i.Book == fragment.Book)
                                                             .Where(i => i.Editor == fragment.Editor)
                                                             .FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Fragments.Remove(_fragment);
                    _context.SaveChanges();
                    return Ok(_fragment);
                }

            }
            catch (Exception ex)
            {
                // log
            }
            return NotFound(fragment);
        }

        
    }
}