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
using Go2Go.Model.ViewModels;

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

        [HttpGet]
        [Route("get-ledger-balance/{userId:int}")]
        public async Task<ActionResult<DriverBalanceViewModel>> GetLedgerBalanceForUser(int userId)
        {
            var resultx = await _accountService.GetLedgerBalanceForUser(userId);
            return Ok(resultx);
        }

        [HttpGet]
        [Route("get-ledger-balance-for-key/{userkey}")]
        public async Task<ActionResult<DriverBalanceViewModel>> GetLedgerBalanceForKey(string userkey)
        {
            var resultx = await _accountService.GetLedgerBalanceForKey(userkey);
            return Ok(resultx);
        }
        [HttpGet]
        [Route("get-ledger-entries/{userkey}")]
        public async Task<ActionResult<UserLedgerViewModel>> GetLedgerEntries(string userkey)
        {
            var resultx = await _accountService.GetLedgerEntry(userkey);
            return Ok(resultx);
        }
    }
}
