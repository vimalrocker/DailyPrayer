// Copyright 2025-2025 NXGN Management, LLC. All Rights Reserved.

using Api.Models;
using Shared.DTO;

namespace Api.Services;

public class UserService
{
    public async Task<UserResponse> GetUserByEmail(Supabase.Client supabase, UserRequest request)
    {
        // A result can be fetched like so.
        var response =  await supabase.From<UserProfile>().Where(n=>n.Email == request.Email).Get();
        var userdetails = new UserResponse
        {
            Id = response?.Model?.Id,
            Name = response.Model.DisplayName
        };

        return userdetails;
    } 
}