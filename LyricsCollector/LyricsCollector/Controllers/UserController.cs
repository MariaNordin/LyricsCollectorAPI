﻿using LyricsCollector.Context;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost("Authenticate")]
        public IActionResult Login(UserPostModel payload)
        {
            var result = _userService.Authenticate(payload);

            if (result == null) return BadRequest(new { Message = "Username or password was incorrect." });

            return Ok(result);
        }

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public IActionResult GetUsers()
        //{
        //    var users = _userService.G
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Register(UserPostModel payload)
        {
            var result = _userService.RegisterUser(payload);
            return Ok(new 
            {
                Status = "Registered",
                result
            });
        }
    }
}