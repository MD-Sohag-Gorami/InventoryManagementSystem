using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;


namespace InventoryManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        #region Ctor
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        #region Method
        #region Create User
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel viewModel)
        {
            var user = new IdentityUser()
            {
                UserName = viewModel.Email,
                Email = viewModel.Email
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);

            return result;
        }
        #endregion
        #region Signin Method

        public async Task<SignInResult> PasswordSignInAsyn(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }
        #endregion
        #region Signout Method

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion
        #endregion
    }
}
