using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TCommentaryController : Controller
    {
        private readonly OSCC_DBContext _context;

        public TCommentaryController(OSCC_DBContext context)
        {
            _context = context;
        }

        public IActionResult GetByTextID([FromQuery] int textID, int lineNumber)
        {
            return Json(_context.TCommentary.ToList().Where(i => i.Book == textID)
                                                        .Where(i => i.LineStart <= lineNumber)
                                                        .Where(i => i.LineEnd >= lineNumber)
                                                        );
            // return Json(_context.Fragments.ToList().Where(i => i.FragmentName == fragmentID).Where(i => i.Editor == editorID).Where(i => i.Book == bookID).Select(x => x.Id));

        }

        // [HttpPost]
        // [Route("Create")]
        // public IActionResult Create([FromBody] FTranslations translation)
        // {
        //     try
        //     {
        //         if (ModelState.IsValid)
        //         {
        //             // Check if fragment already has a differences field.
        //             FTranslations _translation = _context.FTranslations.ToList().Where(i => i.Fragment == translation.Fragment)
        //                                                      .FirstOrDefault();
        //             // If not, add a new differences row to the database
        //             if(_translation == null){
        //                 _context.FTranslations.Add(translation);
        //                 _context.SaveChanges();
        //                 return Ok(translation);                        
        //             }
        //             // Else, change the differences field for this fragment.
        //             else{
        //                 _translation.Translation = translation.Translation;
        //                 _context.SaveChanges();
        //                 return Ok(translation);
        //             }
        //         }

        //     }
        //     catch (Exception ex)
        //     {
        //         return Ok(ex);// log
        //     }

        //     return Conflict(translation);
        // }

    }
}