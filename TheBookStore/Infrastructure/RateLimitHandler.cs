using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TheBookStore.Models;

namespace TheBookStore.Infrastructure
{
    public class RateLimitHandler : DelegatingHandler
    {
        const int timeout = 20;
        const int limit = 5;
        const string basicAuthResponseHeader = "WWW-Authenticate";
        const string basicAuthResponseHeaderValue = "Basic";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage errorMessage;
            if (request.Headers.Authorization == null)
            {
                errorMessage = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization token required!");
                errorMessage.Headers.Add(basicAuthResponseHeader, basicAuthResponseHeaderValue);

                return errorMessage;
            }

            string token = request.Headers.Authorization.Parameter;
            string query = request.RequestUri.PathAndQuery;
            string key = string.Format("{0}:{1}", query, token);
            RateLimit hit = (RateLimit)HttpRuntime.Cache.Get(key);

            if (hit == null)
            {
                hit = new RateLimit(key, limit, timeout);
            }

            if (!hit.CheckLimit())
            {
                string message = string.Format("Rate-limit of {0} reached! Try again in {1} second(s)", hit.Limit, hit.RemainingSeconds);
                errorMessage = request.CreateErrorResponse((HttpStatusCode)429, message);
                errorMessage.ReasonPhrase = message;
            }
            else
            {
                errorMessage = await base.SendAsync(request, cancellationToken);
            }

            errorMessage.Headers.Add("X-Rate-Limit-Limit", hit.Limit.ToString());
            errorMessage.Headers.Add("X-Rate-Limit-Remaining", hit.RemainingHits.ToString());
            errorMessage.Headers.Add("X-Rate-Limit-Reset", hit.Reset.ToUniversalTime().ToString());

            return errorMessage;
        }
    }
}