using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public interface IProductModelFactory
    {
        Task<ProductViewModel> PrepareProductViewModelAsync(ProductViewModel viewModel);
        Task<List<ProductViewModel>> PrepareAllProductsByIdAsync();
        Task<ProductViewModel> PrepareProductByIdAsync(int id);
    }
}