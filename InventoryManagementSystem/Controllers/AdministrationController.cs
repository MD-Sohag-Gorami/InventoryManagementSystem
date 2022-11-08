using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Authorize ( Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService _administrationService;

        public AdministrationController(IAdministrationService administrationService
                                    )
        {
            _administrationService = administrationService;
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _administrationService.CreateRoleAysnc(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null) return NotFound();
            await _administrationService.DeleteRoleByIdAsync(id);
           
             return RedirectToAction("ListRoles", "Administration"); 
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRole(string userId)
        {
            if(userId == null) return NotFound();    
            ViewBag.UserId = userId;
            var model = await _administrationService.GetRolesByUserIdAsync(userId);

            if (model.Count() > 0) return View(model);
            return View("NotFound");
        }

        [HttpPost]
       /* public async Task<IActionResult> ManageUserRole(List<UserRolesViewModel> model,string userId)
        {
            if(ModelState.IsValid)
            {
                var result = await _administrationService.DeleteRolesByUserIdAsync(model,userId);
            }
            if (userId == null) return NotFound();
            ViewBag.UserId = userId;
            var model = await _administrationService.GetRolesByUserIdAsync(userId);

            if (model.Count() > 0) return View(model);
            return View("NotFound");
        }*/

        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var roles =  await _administrationService.ListOfRolesAysnc();
            return View(roles);
        }
        [HttpGet]

        public async Task<IActionResult> EditRole(string? id)
        {
            if (id == null) return NotFound();

            var role = await _administrationService.GetUserByRoleIdAsync(id);
            if (role == null) return NotFound();
         
            return View(role);
        }

        [HttpPost]

        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {

            if(ModelState.IsValid)
			{
                await _administrationService.UpdateRoleByIdAsync(model);

                return RedirectToAction("ListRoles", "Administration");
            }
          
            return View(model);
          
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var result = await _administrationService.GetRoleByIdAsync(roleId);
            if (result == null) return View("NotFound");
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            if (model == null || roleId==null) return View("Not found Valid Model list");
            var resutl = await _administrationService.UpdateUsersInRoleByIdAysnc(model,roleId);

           return RedirectToAction("EditRole", new {Id = roleId});
        }
    } 
}
