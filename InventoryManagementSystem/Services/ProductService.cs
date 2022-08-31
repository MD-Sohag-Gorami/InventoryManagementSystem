using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProductService
    {
        #region Ctor
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region GetAllProduct
        public IList<ProductModel> GetAllProducts()
        {
            var products = _db.Product.ToList();
            if(products == null) return new List<ProductModel>();
            return products;
        }
        #endregion

        #region GerProductById
        public ProductModel GetProductById(int id)
        {
            var product = _db.Product.Find(id);
            if (product == null)
            {
                return new ProductModel();
            }
            return product;
        }
        #endregion

        #region Update Product

        public void UpdateProduct(ProductModel product)
        {
            if (product.Image != null)
            {
                string image = "Images/ProImage/";
                image += Guid.NewGuid().ToString() + " - " + product.Image.FileName;
                product.ImageUrl = "/" + image;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, image);
                product.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
            _db.Product.Update(product);
            _db.SaveChanges();
        }
        #endregion

        #region Delete Product
        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            _db.Product.Remove(product);
            _db.SaveChanges();
        }
        #endregion

        #region Add Product
        public void InsertProduct(ProductModel product)
        {

            if (product.Image != null)
            {
                string image = "Images/ProImage/";
                image += Guid.NewGuid().ToString() + " - " + product.Image.FileName;

                product.ImageUrl = "/" + image;
              
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, image);

                product.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
            _db.Product.Add(product);
            _db.SaveChanges();
        }
        #endregion

      
    }
}
