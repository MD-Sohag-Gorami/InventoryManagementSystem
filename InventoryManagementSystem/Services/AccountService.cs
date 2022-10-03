using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Identity;


namespace InventoryManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        #region Ctor
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
                              ,IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        #endregion
        #region Method
    
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
      
        public async Task<SignInResult> PasswordSignInAsyn(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel passwordModel)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, passwordModel.CurrentPassword,
                                                   passwordModel.NewPassword);
            return result;
        }

        #endregion
    }
}
