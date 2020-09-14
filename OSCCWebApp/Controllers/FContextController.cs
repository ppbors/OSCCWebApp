using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FContextController : Controller
    {
        private readonly OSCC_DEVContext _context;

        public FContextController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByFragmentID([FromQuery] int fragmentID)
        {
            return Json(_context.FContext.ToList().Where(i => i.Fragment == fragmentID));
        }

        [HttpPost]
        public IActionResult Create([FromBody] FContext context)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.FContext.Add(context);
                    _context.SaveChanges();
                    return Ok(context);
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(context);
        }
    }
}