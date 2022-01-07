using Go2Go.Core.Exceptions;
using Go2Go.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Go2Go.Web.Models
{
    public static class StatusCodeExceptionExtension
    {
        public static HttpStatusCode GetStatusCodeByException(this Exception exception)
        {
            switch (exception)
            {
                case NotFoundException _:
                    return HttpStatusCode.NotFound;
                case ForbiddenException _:
                    return HttpStatusCode.Forbidden;
                case ValidationException _:
                case MultiValidationException _:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
