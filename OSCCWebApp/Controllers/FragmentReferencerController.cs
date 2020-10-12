using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FragmentReferencerController : Controller
    {
        private readonly OSCC_NEWContext _context;

        public FragmentReferencerController(OSCC_NEWContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetReferencerID([FromQuery] int fragmentID, [FromQuery] int editorID, [FromQuery] int bookID)
        {
            return Json(_context.FragmentReferencer.ToList().Where(i => i.FragmentNo == fragmentID).Where(i => i.Editor == editorID).Where(i => i.Book == bookID).Select(x => x.Id));
        }

        [HttpPost]
        [Route("SetPublishFlag")]
        public IActionResult SetPublishByAttr([FromBody] FragmentReferencer fragmentReferencer)
        {
            FragmentReferencer _fragmentReferencer = _context.FragmentReferencer.ToList().Where(i => i.Editor == fragmentReferencer.Editor)
                                                                                         .Where(i => i.Book == fragmentReferencer.Book)
                                                                                         .Where(i => i.FragmentNo == fragmentReferencer.FragmentNo)
                                                                                         .FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _fragmentReferencer.Published = fragmentReferencer.Published;
                    _context.SaveChanges();
                    return Ok(_fragmentReferencer);
                }

            }
            catch (Exception ex)
            {
                // log
            }
            return NotFound(fragmentReferencer);
        }
    }
}