using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public interface IProductModelFactory
    {
        Task<ProductViewModel> PrepareProductViewModelAsync(ProductViewModel viewModel);
        Task<List<ProductViewModel>> PrepareAllProductsAsync(string productSearch = "", int warehouseSearch = 0, DateTime dateWiseProductSearch = new DateTime());
        Task<ProductViewModel> PrepareProductByIdAsync(int id);
    }
}