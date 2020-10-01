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
        private readonly OSCC_DEVContext _context;

        public FTranslationsController(OSCC_DEVContext context)
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
                    _context.FTranslations.Add(translation);
                    _context.SaveChanges();
                    return Ok(translation);
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(translation);
        }

    }
}