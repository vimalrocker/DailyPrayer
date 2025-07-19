// Copyright 2025-2025 NXGN Management, LLC. All Rights Reserved.

using System.Net.Http.Json;
using Shared.DTO;

namespace Client.Service;

public class UserApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public UserApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

    }
    
    public async Task<UserResponse> GetUserByEmailAsync(string email)
    {
        try
        {
            // Call the Azure Function with GET request

            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            var response = await _httpClient.GetAsync($"{apiBaseUrl}UserFunction");


            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserResponse>();
                return result;
            }
            
            throw new HttpRequestException($"Request failed with status: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error calling Azure Function: {ex.Message}");
        }
    }
}