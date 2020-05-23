using Microsoft.AspNetCore.Http;
using System;

namespace WebUI.Extensions
{
    public static class HttpRequestExtension
    {
        public static Uri GetReferer(this HttpRequest request) => new Uri(request.Headers["Referer"].ToString());
    }
}
