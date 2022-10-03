using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
