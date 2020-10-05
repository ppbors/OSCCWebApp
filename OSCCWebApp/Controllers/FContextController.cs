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
        [Route("Create")]
        public IActionResult Create([FromBody] FContext myContext)
        {

            // Check if fragment already has a differences field.
            FContext _myContext = _context.FContext.ToList().Where(i => i.Fragment == myContext.Fragment)
                                                        .FirstOrDefault();

            try
            {
                if (ModelState.IsValid)
                {


                    if(_myContext == null){
                        _context.FContext.Add(myContext);
                        _context.SaveChanges();
                        return Ok(myContext);
                    }
                    else{
                        _myContext.ContextAuthor = myContext.ContextAuthor;
                        _myContext.Context = myContext.Context;
                        _context.SaveChanges();
                        return Ok(myContext);
                    }
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(myContext);
        }


        
    }
}