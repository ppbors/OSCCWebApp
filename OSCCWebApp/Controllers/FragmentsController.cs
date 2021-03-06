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
        private readonly OSCC_DBContext _context;

        public FragmentsController(OSCC_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult FromBookID([FromQuery] int bookID)
        {
            return Json(_context.Fragments.ToList().Where(i => i.Book == bookID));
        }

        [HttpGet]
        [Route("Hello")] //FIXME: change name
        public IActionResult GetReferencerID2([FromQuery] string fragmentID, [FromQuery] int editorID, [FromQuery] int bookID)
        {
            return Json(_context.Fragments.ToList().Where(i => i.FragmentName == fragmentID).Where(i => i.Editor == editorID).Where(i => i.Book == bookID).Select(x => x.Id));
        }

        [HttpPost] //FIXME: This must be an update statement: if it is there, update it accordingly.
        public IActionResult Create([FromBody] Fragments fragment)
        {
            // Check if fragment already has a differences field.
            Fragments _fragment = _context.Fragments.ToList().Where(i => i.FragmentName == fragment.FragmentName)
                                                             .Where(i => i.LineName == fragment.LineName)
                                                             .Where(i => i.Book == fragment.Book)
                                                             .Where(i => i.Editor == fragment.Editor)
                                                             .FirstOrDefault();            
            try
            {
                if (ModelState.IsValid)
                {
                    // If not, add a new fragment row to the database
                    if(_fragment == null){
                        _context.Fragments.Add(fragment);
                        _context.SaveChanges();
                        return Ok(fragment);
                    }
                    // Else, change the linecontent field for this fragment.
                    else{
                        _fragment.LineContent = fragment.LineContent;
                        _context.SaveChanges();
                        return Ok(fragment);
                    }



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

        [HttpPost]
        [Route("SetPublishFlag")] //TODO: Does not work.
        public IActionResult SetPublishByAttr([FromBody] Fragments fragment)
        {
            Fragments _fragment = _context.Fragments.ToList().Where(i => i.Editor == fragment.Editor)
                                                             .Where(i => i.Book == fragment.Book)
                                                             .Where(i => i.FragmentName == fragment.FragmentName)
                                                             .FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _fragment.Published = fragment.Published;
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