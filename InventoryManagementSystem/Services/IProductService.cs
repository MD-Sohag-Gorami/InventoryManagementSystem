using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        void DeleteProduct(int id);
        IList<ProductModel> GetAllProducts();
        ProductModel GetProductById(int id);
        void UpdateProduct(ProductModel product);
        void InsertProduct(ProductModel product);
       
    }
}