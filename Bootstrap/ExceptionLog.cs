using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IdentityServerAPI.Bootstrap
{
    public static class ExceptionLog
    {
        public static async Task<string> LogAsync(this Exception exception, HttpContext httpContext)
        {
            string requestBodyContent = string.Empty;
            using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8))
                requestBodyContent = await reader.ReadToEndAsync();
            httpContext.Items.Add("EXCEPTION", new Dictionary<string, object>()
            {
                {
                    "TraceIdentifier",
                     httpContext.TraceIdentifier
                },
                {
                    "Message",
                     exception.Message
                },
                {
                    "ExceptionType",
                     exception.GetType().ToString()
                },
                {
                    "ExceptionSource",
                     exception.Source
                },
                {
                    "StackTrace",
                     exception.StackTrace
                },
                {
                    "InnerExceptionMessage",
                    exception.InnerException != null ?  exception.InnerException.Message :  string.Empty
                },
                {
                    "InnerExceptionStackTrace",
                    exception.InnerException != null ?  exception.InnerException.StackTrace :  string.Empty
                },
                {
                    "RequestBody",
                     requestBodyContent
                },
                {
                    "CreatedDate",
                     DateTime.UtcNow
                }
            });
            return httpContext.TraceIdentifier;
        }
    }
}