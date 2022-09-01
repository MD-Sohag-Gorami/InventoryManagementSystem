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
        public IActionResult Signup()
        {
            return View();
        }
        [Route("Signup")]
        [HttpPost]
        public async Task <IActionResult> Signup(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {

                var result = _accountService.CreateUser(userModel);
               
                if (result.IsCompletedSuccessfully)
                {
                    Console.WriteLine("Asche ");
                    return View ("Product","Index");
                }

                ModelState.AddModelError("", "Invalid login credendial ");


                ModelState.Clear();
                
            }
            return View(userModel);

        }
    }
}
