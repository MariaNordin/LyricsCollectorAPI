using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    public class CollectionController : ControllerBase
    {
        // GET: Hämta alla listor
        // GET: Hämta en lista
        // POST: Skapa ny lista
        // DELETE: Ta bort lista
        // POST: Lägg till låt i lista
        // DELETE: Ta bort låt i lista


        private readonly ICollectionService _collectionService;
        //private Collection _collection;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpPost("Save")]
        //public async Task<IActionResult> SaveToCollectionAsync([FromBody] LyricsResponseModel lyricsRM, int collectionId)
        //{
            //var result = await _lyricsService.SaveCollectionLyricsAsync(lyricsRM, _user.Id, collectionId);
            // Save in collection:
            // check if lyrics in db : lägg tll annars

            //if (result)
            //{
            //    return Ok(new
            //    {
            //        Status = "Saved lyrics to list"
            //    });
            //}
            //return BadRequest(new { Status = "Saving lyrics to list failed." });

        //}
        //[HttpGet]
        //public async Task<IActionResult> GetAllUsersLists([FromBody] UserPostModel userPM)
        //{

        //}
    }
}
