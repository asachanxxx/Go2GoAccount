using Go2Go.Implementations.Services;
using Go2Go.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Go2Go.Web.Controllers.Security
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityEngineController : ControllerBase
    {
        private readonly IAuthService _authService;

        public SecurityEngineController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("add-guser")]
        public async Task<ActionResult<int>> AddGUser(UserViewModel userViewModel)
        {
            var resultx = await _authService.AddGUser(userViewModel);
            return await Task.FromResult<int>(resultx);
        }

        [HttpPost]
        [Route("update-guser")]
        public async Task<ActionResult<int>> UpdateGUser(UserViewModel userViewModel)
        {
            var resultx = await _authService.UpdateGUser(userViewModel);
            return await Task.FromResult<int>(resultx);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<int>> SignIn(SignInViewModel signInViewModel)
        {
            var resultx = await _authService.SignIn(signInViewModel);
            return await Task.FromResult<int>(resultx);
        }

    }
}
