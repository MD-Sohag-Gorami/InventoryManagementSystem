using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<ProductModel> GetAllProducts()
        {
            var products = _db.Product.ToList();
            return products;
        }

        public ProductModel GetProductById(int id)
        {
            var product = _db.Product.Find(id);
            if (product == null)
            {
                return new ProductModel();
            }
            return product;
        }

        public void UpdateProduct(ProductModel product)
        {
            _db.Product.Update(product);
            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            _db.Product.Remove(product);
            _db.SaveChanges();
        }

        public void InsertProduct(ProductModel product)
        {
            _db.Product.Add(product);
            _db.SaveChanges();
        }

        public ProductModel DetailProduct(int id)
        {
            var product = _db.Product.Find(id);
            if (product == null)
            {
                return new ProductModel();
            }
            return product;
        }

        public ProductModel DetailProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}
