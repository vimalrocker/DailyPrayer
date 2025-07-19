// Copyright 2025-2025 NXGN Management, LLC. All Rights Reserved.

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Api;

public class CorsMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var request = await context.GetHttpRequestDataAsync();
        
        if (request != null)
        {
            // Handle preflight requests
            if (request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                var response = request.CreateResponse(HttpStatusCode.OK);
                AddCorsHeaders(response);
                context.GetInvocationResult().Value = response;
                return;
            }
        }

        await next(context);

        // Add CORS headers to all responses
        if (context.GetInvocationResult().Value is HttpResponseData httpResponse)
        {
            AddCorsHeaders(httpResponse);
        }
    }

    private static void AddCorsHeaders(HttpResponseData response)
    {
        response.Headers.Add("Access-Control-Allow-Origin", "*");
        response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        response.Headers.Add("Access-Control-Max-Age", "86400");
    }
}