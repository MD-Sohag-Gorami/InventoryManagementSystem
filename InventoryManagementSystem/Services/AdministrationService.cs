using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Services
{
    public class AdministrationService : IAdministrationService
    {
        #region Ctor
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationService(RoleManager<IdentityRole> roleManager,
                                     UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
             _userManager = userManager;
        }
        #endregion
        #region Methods

        public async Task<IdentityResult> CreateRoleAysnc(CreateRoleViewModel model)
        {
            IdentityRole role = new IdentityRole
            {
                Name = model.RoleName
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            return result;
        }
        public async Task DeleteRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
         
            await _roleManager.DeleteAsync(role);
           
        }
        public async Task<List<IdentityRole>> ListOfRolesAysnc()
        {
            var result = _roleManager.Roles;
            return result.ToList();
           
        }
        public async Task<EditRoleViewModel> GetUserByRoleIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            foreach(var user in _userManager.Users.ToList())
            {
                var resutl = await _userManager.IsInRoleAsync(user, role.Name);
                if (resutl)
                {
                    model.Users.Add(user.UserName);
                }
            }
            return model;
        }
        public async Task<List<UserRolesViewModel>> GetRolesByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<UserRolesViewModel>();
            var model = new List<UserRolesViewModel>();
            foreach(var role in _roleManager.Roles.ToList())
            {
                if (role == null) continue;
                var UserRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };
                var checkUser = await _userManager.IsInRoleAsync(user, role.Name);
                if (checkUser)
                {
                    UserRolesViewModel.IsSelected = true;
                }
                else
                    UserRolesViewModel.IsSelected = false;
                model.Add(UserRolesViewModel);
            }
            return model;
            
        }
      /*  public async Task<IdentityResult> DeleteRolesByUserIdAsync(List<UserRolesViewModel> model,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) return new IdentityResult();
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRoleAsync(user,roles);
            if(!result.Succeeded)
            {
                //Can't remove user existing user roles 
                return result;
            }
            var selcet
            resutl = await _userManager.A
            
        }*/

        public async Task UpdateRoleByIdAsync(EditRoleViewModel model)
		{
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null) return;
            role.Name = model.RoleName;
            await _roleManager.UpdateAsync(role);

        }
        public async Task<List<UserRoleViewModel>> GetRoleByIdAsync(string roleId)
        {
           if(roleId==null) return new List<UserRoleViewModel>();
            var role = await _roleManager.FindByIdAsync(roleId);
            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                var check = await _userManager.IsInRoleAsync(user, role.Name);
                if(check)
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);

            }
            return model;
        }

        public async Task<IdentityResult> UpdateUsersInRoleByIdAysnc(List<UserRoleViewModel> model,string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return new IdentityResult();
            IdentityResult result = new IdentityResult();
            for (int cnt = 0; cnt < model.Count;cnt++)
            {
                var user = await _userManager.FindByIdAsync(model[cnt].UserId);
                 result = null;
                var isUserInRole = false;
                isUserInRole = await _userManager.IsInRoleAsync(user, role.Name);

                if (model[cnt].IsSelected && !isUserInRole)
                {
                    result = await _userManager.AddToRoleAsync(user,role.Name);
                }
                else if(!model[cnt].IsSelected && isUserInRole)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded)
                {
                    if (cnt < model.Count - 1) continue;
                    else return result;
                        
                }
               
            }
            return result;
        }

        #endregion
    }
}
