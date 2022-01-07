using Microsoft.AspNetCore.Http;
using System;

namespace Go2Go.Web.Authentication
{
    public static class HttpRequestExtension
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }

        public static bool IsApiCall(this HttpRequest request)
        {
            return request.Path.HasValue && request.Path.StartsWithSegments("/api");
        }
    }
}
