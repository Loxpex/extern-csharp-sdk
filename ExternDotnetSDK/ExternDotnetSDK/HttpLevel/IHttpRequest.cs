#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.HttpLevel.Models;

namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpRequest
    {
        /// <summary>
        /// Set accept header to the request
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        IHttpRequest Accept(string contentType);

        /// <summary>
        /// Set authorization header to the request
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IHttpRequest Authorization(string scheme, in Base64String parameter);

        /// <summary>
        /// Send a request and ensures successful response 
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="ignoreResponseErrors">a delegate that allows you to avoid throwing an exception in case of a response with errors.</param>
        /// <returns>a successful response or response with errors which allowed by an inner error handler or by <paramref name="ignoreResponseErrors"/>.</returns>
        Task<IHttpResponse> SendAsync(TimeSpan? timeout = null, Func<IHttpResponse, bool>? ignoreResponseErrors = null);
    }
}