using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSCCWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EditorsController : Controller
    {
        private readonly OSCC_DBContext _context;

        public EditorsController(OSCC_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetByBookID([FromQuery] int bookID)
        {
            return Json(_context.Editors.ToList().Where(i => i.Book == bookID));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Editors editor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Editors.Add(editor);
                    _context.SaveChanges();
                    return Ok(editor);
                }

            }
            catch (Exception ex)
            {
                // log
            }

            return Conflict(editor);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteByName([FromBody] Editors editor) //FIXME: DeleteById new name
        {
            Editors _editor = _context.Editors.ToList().Where(i => i.Id == editor.Id).FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Editors.Remove(_editor);
                    _context.SaveChanges();
                    return Ok(_editor);
                }

            }
            catch (Exception ex)
            {
                // log
            }
            return NotFound(editor);
        }

        [HttpPost]
        [Route("SetMainFlag")]
        public IActionResult SetMainByID([FromBody] Editors editor)
        {
            Editors _editor = _context.Editors.ToList().Where(i => i.Id == editor.Id).FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    _editor.MainEditor = editor.MainEditor;
                    _context.SaveChanges();
                    return Ok(_editor);
                }

            }
            catch (Exception ex)
            {
                // log
            }
            return NotFound(editor);
        }
    }
}