using Go2Go.Data.Context;
using Go2Go.Implementations.Repositories;
using Go2Go.Implementations.Services;
using Go2Go.Model;
using Go2Go.Web.Implimentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Go2Go.Core.Extentions;
using Go2Go.Core;

namespace Go2Go.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLedgerController : BaseController<UserLedger, EfCoreUserLedgerRepository>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<UserLedgerController> _logger;

        public UserLedgerController(EfCoreUserLedgerRepository repository, IAccountService accountService, ILogger<UserLedgerController> logger) : base(repository)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [Route("get-ledger-balance/{userId}")]
        public async Task<ActionResult<bool>> GetLedgerBalanceForUser(int userId)
        {
            var resultx = await _accountService.GetLedgerBalanceForUser(userId);
            return Ok(resultx);
        }

        [HttpPost]
        [Route("get-ledger-balance-for-key/{userkey}")]
        public async Task<ActionResult<bool>> GetLedgerBalanceForKey(string userkey)
        {
            var resultx = await _accountService.GetLedgerBalanceForKey(userkey);
            return Ok(resultx);
        }
    }
}
