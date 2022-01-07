using Go2Go.Data.Context;
using Go2Go.Implementations.Repositories;
using Go2Go.Model;
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
    public class UsersController : BaseController<User, EfCoreUserRepository>
    {
        public UsersController(EfCoreUserRepository repository) : base(repository)
        {

        }
    }
}
