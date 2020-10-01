using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FCommentaryController : Controller
    {
        private readonly OSCC_DEVContext _context;

        public FCommentaryController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByFragmentID([FromQuery] int fragmentID)
        {
            return Json(_context.FCommentary.ToList().Where(i => i.Fragment == fragmentID));
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] FCommentary commentary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.FCommentary.Add(commentary);
                    _context.SaveChanges();
                    return Ok(commentary);
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(commentary);
        }


    }



}