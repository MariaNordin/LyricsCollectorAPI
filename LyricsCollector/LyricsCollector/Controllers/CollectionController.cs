using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LyricsCollector.Controllers
{
    public class CollectionController : ControllerBase
    {
        
        private readonly ICollectionService _collectionService;
        private readonly IDbCollectionHelper _dbCollection;
        //private Collection _collection;

        public CollectionController(ICollectionService collectionService, IDbCollectionHelper dbCollection)
        {
            _collectionService = collectionService;
            _dbCollection = dbCollection;
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
