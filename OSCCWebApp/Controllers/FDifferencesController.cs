using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FDifferencesController : Controller
    {

        private readonly OSCC_DEVContext _context;

        public FDifferencesController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByFragmentID([FromQuery] int fragmentID)
        {
            return Json(_context.FDifferences.ToList().Where(i => i.Fragment == fragmentID));
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] FDifferences differences)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if fragment already has a differences field.
                    FDifferences _differences = _context.FDifferences.ToList().Where(i => i.Fragment == differences.Fragment)
                                                             .FirstOrDefault();
                    // If not, add a new differences row to the database
                    if(_differences == null){
                        _context.FDifferences.Add(differences);
                        _context.SaveChanges();
                        return Ok(differences);                        
                    }
                    // Else, change the differences field for this fragment.
                    else{
                        _differences.Differences = differences.Differences;
                        _context.SaveChanges();
                        return Ok(differences);
                    }
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(differences);
        }


    }
}