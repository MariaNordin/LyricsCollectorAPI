using Microsoft.AspNetCore.Mvc;

namespace LyricsCollector.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Hämta alla listor
        // GET: Hämta en lista
        // POST: Skapa ny lista
        // POST: Lägg till låt i lista
        // DELETE: Ta bort lista 

        public IActionResult Index()
        {
            return View();
        }
    }
}
