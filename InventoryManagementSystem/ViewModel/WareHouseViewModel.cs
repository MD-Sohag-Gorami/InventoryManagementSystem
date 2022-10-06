using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModel
{
    public class WareHouseViewModel
    {
        public WareHouseViewModel()
        {
            ProductList = new List<ProductViewModel>();
        }
        [Key]
        public int Id { get; set; }
        [DisplayName("Please Enter WareHouse Name : ")]
        [Required]
        public string? Name { get; set; }
        [DisplayName("Please Enter WareHous Location: ")]
        [Required]
        public string? Location { get; set; }

        [Display(Name = "Enter WareHous Img ")]
        public IFormFile? ImgByte { get; set; }
        public string? ImgByteUrl { get; set; }

        public List<ProductViewModel> ProductList { get; set; }


    }
}
