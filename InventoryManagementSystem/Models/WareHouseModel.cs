using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class WareHouseModel
    {
        public WareHouseModel()
        {
            ProductList = new List<ProductModel>();
        }
        public int Id { get; set; }
       public string? Name { get; set; }
       public string? Location { get; set; }
       public byte[]? ImgByte { get; set; }

       public List<ProductModel> ProductList { get; set; }


    }
}
