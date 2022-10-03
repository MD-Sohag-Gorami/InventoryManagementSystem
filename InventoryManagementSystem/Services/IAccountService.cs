using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Services
{
    public interface IAccountService
    {
        Task <IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsyn(SignInModel signInModel);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel passwordModel);
    }
}