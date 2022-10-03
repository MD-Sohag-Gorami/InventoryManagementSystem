using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModel
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password),Display(Name = "Current password")]
        public string CurrentPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New password")]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Confirm new password")]
        [Compare("NewPassword",ErrorMessage ="bhaiya password mile nai !! ")]
        public string ConfirmNewPassword { get; set; }

    }
}
