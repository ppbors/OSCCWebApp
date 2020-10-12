using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FTranslationsController : Controller
    {
        private readonly OSCC_NEWContext _context;

        public FTranslationsController(OSCC_NEWContext context)
        {
            _context = context;
        }

        public IActionResult GetByFragmentID([FromQuery] int fragmentID)
        {
            return Json(_context.FTranslations.ToList().Where(i => i.Fragment == fragmentID));
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] FTranslations translation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if fragment already has a differences field.
                    FTranslations _translation = _context.FTranslations.ToList().Where(i => i.Fragment == translation.Fragment)
                                                             .FirstOrDefault();
                    // If not, add a new differences row to the database
                    if(_translation == null){
                        _context.FTranslations.Add(translation);
                        _context.SaveChanges();
                        return Ok(translation);                        
                    }
                    // Else, change the differences field for this fragment.
                    else{
                        _translation.Translation = translation.Translation;
                        _context.SaveChanges();
                        return Ok(translation);
                    }
                }

            }
            catch (Exception ex)
            {
                return Ok(ex);// log
            }

            return Conflict(translation);
        }

    }
}