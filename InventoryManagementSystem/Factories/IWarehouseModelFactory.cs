using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public interface IWarehouseModelFactory
    {
        Task<List<WareHouseViewModel>> PrepareAllWareHoueAsync();
        Task<WareHouseViewModel> PrepareWareHouseByIdAsync(int id);

       
    }
}