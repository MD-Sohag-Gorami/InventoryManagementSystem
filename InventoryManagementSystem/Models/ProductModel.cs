using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class ProductModel
    {
        [Key]
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public int ProductQnty { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime  CreateDateOn { get; set; }
        public int WareHouseId { get; set; }
        public virtual WareHouseModel? WareHouseModel { get; set; }
       
       

    }
}
