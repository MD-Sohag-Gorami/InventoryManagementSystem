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
        [HttpGet]
        public IActionResult Signup()
        {
            SignUpUserModel model = new SignUpUserModel();
            return View(model);
        }
    
        [HttpPost]
        public async Task <IActionResult> Signup(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {

                var result = _accountService.CreateUserAsync(userModel);
               
                if (result.IsCompletedSuccessfully)
                {
         
                    return RedirectToAction ("Index","Product");
                }

                ModelState.AddModelError("", "Invalid login credendial ");
                
            }
            return View(userModel);

        }
    }
}
