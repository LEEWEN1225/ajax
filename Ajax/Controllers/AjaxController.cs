using AJAX.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AJAX.Controllers
{
    public class AjaxController : Controller
    {
        NorthwindContext _context;
        public AjaxController(NorthwindContext context)         
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JavaScriptGreet()
        {
            return View();
        }

        public IActionResult jQueryGreet()
        {
            return View();
        }

        public IActionResult FetchGreet()
        {
            return View();
        }

        public IActionResult JavaScriptCheckName()
        {
            return View();
        }

        public IActionResult jQueryCheckName()
        {
            return View();
        }

        public IActionResult FetchCheckName()
        {
            return View();
        }

        [HttpGet]
        public string Greet(string Name)
        {
            Thread.Sleep(3000);
            return $"Hello: {Name}!";
        }

        [HttpPost, ActionName("Greet")]
        public string PostGreet(string Name) 
        {
            Thread.Sleep(3000);
            return $"Hello: {Name}!";
        }

        [HttpPost]
        public string FetchPostGreet([FromBody]Parameter p) 
        {
            Thread.Sleep(3000);
            return $"Hello: {p.Name}!";
        }

        [HttpPost]
        public string CheckName(string FirstName)
        {
            bool Exists=_context.Employees.Any(emp=>emp.FirstName==FirstName);
            return Exists ? "true" : "false";
        }

        [HttpPost]
        public string FetchCheckName([FromBody]Employees p)
        {
            bool Exists = _context.Employees.Any(emp => emp.FirstName == p.FirstName);
            return Exists ? "true" : "false";
        }
    }
}
