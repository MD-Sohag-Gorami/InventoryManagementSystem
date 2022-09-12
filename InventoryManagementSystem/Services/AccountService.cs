using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;


namespace InventoryManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        #region CTor
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public  async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new IdentityUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
            var resutl = await _userManager.CreateAsync(user, userModel.Password);

            return resutl;

        }
    }
}
