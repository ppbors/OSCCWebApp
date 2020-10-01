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
                    _context.FApparatus.Add(apparatus);
                    _context.SaveChanges();
                    return Ok(apparatus);
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