using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        Task DeleteProductAsync(int id);
        Task<List<ProductModel>> GetAllProductsAsync(string productSearch = null, int warehouseId = 0, DateTime dateWiseProductSearch = new DateTime());
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<ProductViewModel> GetProductDetailByIdAsync(int id);
        Task UpdateProductAsync(ProductViewModel viewModel);
        Task InsertProductAsync(ProductViewModel viewModel);
        /*    Task<List<ProductModel>> GetSearchProductAsync(string ProductSearch);*/
        Task<List<ProductViewModel>> GetTopProductsAsync(int count);


    }
}