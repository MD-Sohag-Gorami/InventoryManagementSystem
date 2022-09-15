using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Services
{
    public interface IWareHouseService
    {
        Task DeleteWareHouseAsync(int? id);
        Task<List<WareHouseViewModel>> GetAllWareHouseAsync();
        Task<WareHouseViewModel> GetWareHouseByIdAsync(int? id);
        Task InsertWareHouseAsync(WareHouseViewModel viewModel);
        Task UpdateWareHouseAsync(WareHouseViewModel viewModel);
    }
}