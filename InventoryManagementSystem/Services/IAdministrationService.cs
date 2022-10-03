using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Services
{
    public interface IAdministrationService
    {
        Task<IdentityResult> CreateRoleAysnc(CreateRoleViewModel model);

        Task<EditRoleViewModel> GetUserByRoleIdAsync(string id);
        Task DeleteRoleByIdAsync(string id);
        Task<List<UserRolesViewModel>> GetRolesByUserIdAsync(string userId);
        Task<List<IdentityRole>> ListOfRolesAysnc();
        Task UpdateRoleByIdAsync(EditRoleViewModel model);
        Task<List<UserRoleViewModel>> GetRoleByIdAsync(string roleId);
        Task<IdentityResult> UpdateUsersInRoleByIdAysnc(List<UserRoleViewModel> model, string roleId);
    }
}