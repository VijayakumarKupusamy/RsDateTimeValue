 
using HotChocolate;
using HotChocolate.Execution;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace HotChocolate.Stitching.Pipeline
{
    public class StitchingCacheMiddleware
    {
        private readonly RequestDelegate _next; 

        private static ConcurrentDictionary<string, string> inMemCache = new ConcurrentDictionary<string, string>();
        public StitchingCacheMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            //_httpRequestClient = httpRequestClient;
        }

        public async ValueTask InvokeAsync(IRequestContext context)
        {
            //   if (inMemCache.TryGetValue(context?.DocumentId, out var cachedResult))
            //   {
            //       IReadOnlyDictionary<string, object?> cachedData = JObject.Parse(cachedResult).ToDictionary();
            //       context.Result = QueryResultBuilder.New()
            //                       .SetData(cachedData["data"] as IReadOnlyDictionary<string, object?>)
            //                       .Create();
            //   }
            //   else
            //   {
            ////       context.Result =
            ////await _httpRequestClient.FetchAsync(
            ////    context.Request,
            ////    context.Schema.Name,
            ////    context.RequestAborted)
            ////    .ConfigureAwait(false);

            //      // inMemCache.TryAdd(context.DocumentId, context.Result.ToJson());
            //   } 


            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}
