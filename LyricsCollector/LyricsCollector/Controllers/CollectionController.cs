using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Hämta alla listor
        // GET: Hämta en lista
        // POST: Skapa ny lista
        // DELETE: Ta bort lista 

        public IActionResult Index()
        {
            return View();
        }
    }
}
