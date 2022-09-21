using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        Task DeleteProductAsync(int id);
        Task<List<ProductModel>> GetAllProductsAsync(string productSearch = null);
        Task <ProductModel> GetProductByIdAsync(int id);
        Task UpdateProductAsync(ProductViewModel viewModel);
        Task InsertProductAsync(ProductViewModel viewModel);
    /*    Task<List<ProductModel>> GetSearchProductAsync(string ProductSearch);*/


    }
}