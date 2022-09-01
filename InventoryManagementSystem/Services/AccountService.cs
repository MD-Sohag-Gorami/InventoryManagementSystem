using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;


namespace InventoryManagementSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public  Task<IdentityResult> CreateUser(SignUpUserModel userModel)
        {
            var user = new IdentityUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
           var resutl =  _userManager.CreateAsync(user, userModel.Password);
            return resutl;

        }
    }
}
