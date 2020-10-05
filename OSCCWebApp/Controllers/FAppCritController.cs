using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FApparatusController : Controller
    {
        private readonly OSCC_DEVContext _context;

        public FApparatusController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByFragmentID([FromQuery] int fragmentID)
        {
            return Json(_context.FApparatus.ToList().Where(i => i.Fragment == fragmentID));
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] FApparatus apparatus)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if fragment already has a differences field.
                    FApparatus _apparatus = _context.FApparatus.ToList().Where(i => i.Fragment == apparatus.Fragment)
                                                             .FirstOrDefault();
                    // If not, add a new differences row to the database
                    if(_apparatus == null){
                        _context.FApparatus.Add(apparatus);
                        _context.SaveChanges();
                        return Ok(apparatus);                        
                    }
                    // Else, change the differences field for this fragment.
                    else{
                        _apparatus.Apparatus = apparatus.Apparatus;
                        _context.SaveChanges();
                        return Ok(apparatus);
                    }
                }

            }            
            catch (Exception ex)
            {
                // log
            }

            return Conflict(apparatus);
        }

    }
}