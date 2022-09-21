using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public interface IProductModelFactory
    {
        Task<ProductViewModel> PrepareProductViewModelAsync(ProductViewModel viewModel);
        Task<List<ProductViewModel>> PrepareAllProductsAsync(string productSearch = "");
        Task<ProductViewModel> PrepareProductByIdAsync(int id);
    }
}