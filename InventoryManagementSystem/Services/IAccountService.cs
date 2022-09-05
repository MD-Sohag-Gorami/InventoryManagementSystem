﻿using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Services
{
    public interface IAccountService
    {
        Task <IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsyn(SignInModel signInModel);
        Task SignOutAsync();
    }
}