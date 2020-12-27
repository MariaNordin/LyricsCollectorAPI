using LyricsCollector.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LyricsController : ControllerBase
    {
        private readonly LyricsCollectorDbContext _context;

        public LyricsController(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        // GET: from open api Lyrics.ovh
        [HttpGet("{artist}/{title}")]
        public async Task<IActionResult> Get(string artist, string title)
        {
            var baseAddress = "https://private-anon-db878c4a30-lyricsovh.apiary-proxy.com/v1/";

            string stringResponse;

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{baseAddress}{artist}/{title}"))
                {
                    var responseContent = response.Content;
                    stringResponse = await responseContent.ReadAsStringAsync();
                    //var test = JsonConvert.
                }
            }
            return Ok(stringResponse);
        }

        // GET: api/Lyrics
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllLyricsInCollection(int id)
        {
            var lyrics = _context.CollectionLyrics.Where(l => l.CollectionLyrics))
            return Ok();
        }

        //// GET: LyricsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LyricsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: LyricsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LyricsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LyricsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LyricsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LyricsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
