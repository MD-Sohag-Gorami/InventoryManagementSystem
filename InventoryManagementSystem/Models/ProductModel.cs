using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class ProductModel
    {
        [Key]
        public  int Id { get; set; }
        [DisplayName("Name : ")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Discription : ")]
        [Required]
        public string Description { get; set; }
        [DisplayName("Price : ")]
        [Required]
        public decimal Price { get; set; }
        
        [Display(Name = "Chosse the product image ")]
        [Required]
        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }
    }
}
