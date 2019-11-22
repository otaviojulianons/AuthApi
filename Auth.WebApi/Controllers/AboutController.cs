using Auth.Application.Messages;
using Auth.Application.Services;
using Auth.Common.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace Auth.WebApi.Controllers
{
    [Route("/")]
    public class AboutController : BaseController
    {
        private UserService _userService;

        public AboutController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        public ActionResult Get()
        {
            return Ok(new { version = Assembly.GetExecutingAssembly().GetName().Version.ToString(), machine = Environment.MachineName });
        }
    }
}