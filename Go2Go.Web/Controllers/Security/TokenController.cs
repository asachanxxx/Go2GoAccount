using Go2Go.Web.Authentication;
using Go2Go.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Go2Go.Web.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;

        public TokenController(IJwtTokenManager jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials) {
            var token = _jwtTokenManager.Authenticate(userCredentials.UserName, userCredentials.Password);
            if (string.IsNullOrEmpty(token)) {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
