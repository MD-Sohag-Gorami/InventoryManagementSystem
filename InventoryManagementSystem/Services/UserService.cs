﻿using System.Security.Claims;

namespace InventoryManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetUserId()
        {

            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenicated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
    

}
