using Go2Go.Data.Context;
using Go2Go.Implementations.Repositories;
using Go2Go.Implementations.Services;
using Go2Go.Model;
using Go2Go.Model.ViewModels;
using Go2Go.Web.Implimentation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Go2Go.Web.Controllers.Api
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : BaseController<Trip, EfCoreTripRepository>
    {
        private readonly IAccountService _accountService;

        public TripController(EfCoreTripRepository repository, IAccountService accountService) : base(repository)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("add-trip")]
        public async Task<ActionResult<bool>> AddTrip(TripViewModel entity)
        {

            var resultx = await _accountService.SaveTripRecord(entity);
            return await Task.FromResult<bool>(resultx);

        }
    }
}
