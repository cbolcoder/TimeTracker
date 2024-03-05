﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TimeTracker.API.Services.UserContextService
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<User?> GetUserAsync()
        {
            var httpContextUser = _httpContextAccessor.HttpContext?.User;
            if (httpContextUser == null)
                return null;

            return await _userManager.GetUserAsync(httpContextUser);
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
