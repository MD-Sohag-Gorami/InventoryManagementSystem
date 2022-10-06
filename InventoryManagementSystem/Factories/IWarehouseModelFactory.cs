using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public interface IWarehouseModelFactory
    {
        Task<List<WareHouseViewModel>> PrepareAllWareHoueAsync(string warehouseSearch = "");
        Task<WareHouseViewModel> PrepareWareHouseByIdAsync(int id);

       
    }
}