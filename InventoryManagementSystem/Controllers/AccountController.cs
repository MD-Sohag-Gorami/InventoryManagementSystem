using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Route("signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            SignUpUserModel model = new SignUpUserModel();
            return View(model);
        }
        [Route("signup")]

        [HttpPost]
        public async Task <IActionResult> SignUp(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {

                var result = await _accountService.CreateUserAsync(userModel);
                
                if (result.Succeeded)
                {
         
                    return RedirectToAction ("Index","Product");
                }

                ModelState.AddModelError("", "Invalid login credendial ");
                
            }
            return View(userModel);

        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();  
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(SignInModel signInModel)
        {
            if(ModelState.IsValid)
            {
                var resutl = await _accountService.PasswordSignInAsyn(signInModel);
                if(resutl.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }

                ModelState.AddModelError("", "Invalid login");
            }

            return View(signInModel);
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
             
    }
}
