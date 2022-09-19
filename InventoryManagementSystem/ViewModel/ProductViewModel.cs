using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.ViewModel
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            AvaiableWarehouse = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [DisplayName("Name : ")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Discription : ")]
        [Required]
        public string Description { get; set; }
        [DisplayName("Purchase Price : ")]
        [Required]
        public decimal PurchasePrice { get; set; }

        [DisplayName("Sell Price : ")]
        [Required]
        public decimal SellPrice { get; set; }

        [Display(Name = "Please enter product Quanty ")]
        [Required]
        public int ProductQnty { get; set; }

        [Display(Name = "Choose the product image ")]
 
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "Please set product create date time : ")]
        public DateTime CreateDateOn { get; set; }
        public int WareHouseId { get; set; }
        public IList<SelectListItem> AvaiableWarehouse { get; set; }
    }
}
