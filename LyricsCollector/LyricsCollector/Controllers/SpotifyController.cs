using LyricsCollector.Services.Contracts;
using LyricsCollector.SpotifyClasses;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        private SpotifyToken _token;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        //GET: token
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            _token = await _spotifyService.GetAccessToken();

            return Ok(_token);
        }

        //GET: from spotify api
        //[HttpGet]
        //public async task<iactionresult> getusersauthorization()
        //{
        //    string stringresponse;


        //    var baseurl = new uri("" +
        //        "client_id=7e335aa2c7ed476abf4de347ae1c1ddc&response_type=code&" +
        //        "redirect_uri=http%3a%2f%2flocalhost%3a3000%2f&scope=user-read-private%20user-read-email");


        //    var token = convert.tobase64string(encoding.utf8.getbytes(string.format("{0}:{1}", clientid, clientsecret)));

        //    var client1 = new restclient("");
        //    client1.timeout = -1;
        //    var request = new restrequest(method.post);
        //    request.addheader("authorization", $"basic {token}");
        //    request.addheader("content-type", "application/x-www-form-urlencoded");
        //    request.addparameter("grant_type", "authorization_code");
        //    request.addparameter("code", "mqcbtke...44kn");
        //    request.addparameter("redirect_uri", "");
        //    irestresponse response = client1.execute(request);
        //    console.writeline(response.content);

        //    using (var client = _clientfactory.createclient())
        //    {
        //        client..headers.add("authorization", "basic " + token);

        //        using (var response = await client.getasync(baseurl))
        //        {
        //            var responsecontent = response.content;
        //            stringresponse = await responsecontent.readasstringasync();
        //        }
        //    }

        //    return ok(stringresponse);
        //}
    }
}
