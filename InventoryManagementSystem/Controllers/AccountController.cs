using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Ctor
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region Method

        [Route("signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            SignUpUserModel model = new SignUpUserModel();
            return View(model);
        }
        [Route("signup")]

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _accountService.CreateUserAsync(userModel);

                if (result.Succeeded)
                {

                    return RedirectToAction("LogIn", "Account");
                }

                ModelState.AddModelError("", "Invalid login credendial ");

            }
            return View(userModel);

        }


        [Route("login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var resutl = await _accountService.PasswordSignInAsyn(signInModel);
                if (resutl.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }

                ModelState.AddModelError("", "Invalid login");
            }

            return View(signInModel);
        }



        [Route("logout")]
        //[HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel passwordModel)
        {
            if(ModelState.IsValid)
            {
                var result =await _accountService.ChangePasswordAsync(passwordModel);
                if(result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }

            return View(passwordModel);
        }

        public async Task<IActionResult> UserLogin()
        {
            return View();
        }



    }
#endregion
}
