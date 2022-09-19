using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Services
{
    public interface IWareHouseService
    {
        Task DeleteWareHouseAsync(int? id);
        Task<List<WareHouseModel>> GetAllWareHouseAsync();
        Task<WareHouseModel> GetWareHouseByIdAsync(int? id);
        Task InsertWareHouseAsync(WareHouseViewModel viewModel);
        Task UpdateWareHouseAsync(WareHouseViewModel viewModel);
    }
}