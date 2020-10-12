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
        private readonly OSCC_NEWContext _context;

        public FContextController(OSCC_NEWContext context)
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
            FContext _myContext = _context.FContext.ToList().Where(i => i.Id == myContext.Id)
                                                            .Where(i => i.Fragment == myContext.Fragment)
                                                            // .Where(i => i.ContextAuthor == myContext.ContextAuthor)
                                                            
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
                    // If contextAuthor exists for this fragment, revise it.
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