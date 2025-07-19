// Copyright 2025-2025 NXGN Management, LLC. All Rights Reserved.

using Api.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Supabase;

namespace Api;

public class UserFunction
{
    private readonly ILogger<UserFunction> _logger;
    private readonly Client _supabaseClient;

    public UserFunction(ILogger<UserFunction> logger, Client supabaseClient)
    {
        _logger = logger;
        _supabaseClient = supabaseClient;
    }

    [Function("UserFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        
        var userService = new UserService();
        
  
        // Create a sample request - you'll want to get this from the HTTP request
        var request = new UserRequest { Email = "John@gmail.com" };
        
        var result = await userService.GetUserByEmail(_supabaseClient, request);
        
        return new OkObjectResult(result);
        
    }

}