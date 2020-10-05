using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FReconstructionController : Controller
    {
        private readonly OSCC_DEVContext _context;

        public FReconstructionController(OSCC_DEVContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByFragmentID([FromQuery] int fragmentID)
        {
            return Json(_context.FReconstruction.ToList().Where(i => i.Fragment == fragmentID));
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] FReconstruction reconstruction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if fragment already has a differences field.
                    FReconstruction _reconstruction = _context.FReconstruction.ToList().Where(i => i.Fragment == reconstruction.Fragment)
                                                             .FirstOrDefault();
                    // If not, add a new differences row to the database
                    if(_reconstruction == null){
                        _context.FReconstruction.Add(reconstruction);
                        _context.SaveChanges();
                        return Ok(reconstruction);                        
                    }
                    // Else, change the differences field for this fragment.
                    else{
                        _reconstruction.Reconstruction = reconstruction.Reconstruction;
                        _context.SaveChanges();
                        return Ok(reconstruction);
                    }
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(reconstruction);
        }

    }
}