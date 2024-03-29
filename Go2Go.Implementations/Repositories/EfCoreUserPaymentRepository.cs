﻿using Go2Go.Data.Context;
using Go2Go.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Implementations.Repositories
{
    public class EfCoreUserPaymentRepository : EfCoreRepository<UserPayment, Go2GoContext>
    {
        public EfCoreUserPaymentRepository(Go2GoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
