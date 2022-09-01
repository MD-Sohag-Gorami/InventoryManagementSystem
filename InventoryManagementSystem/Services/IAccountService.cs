using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Services
{
    public interface IAccountService
    {
        Task <IdentityResult> CreateUser(SignUpUserModel userModel);
    }
}