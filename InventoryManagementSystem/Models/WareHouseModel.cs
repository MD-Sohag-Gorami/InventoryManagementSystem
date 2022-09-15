using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class WareHouseModel
    {
     
       public int Id { get; set; }
       public string? Name { get; set; }
       public string? Location { get; set; }
       public byte[]? ImgByte { get; set; }


    }
}
