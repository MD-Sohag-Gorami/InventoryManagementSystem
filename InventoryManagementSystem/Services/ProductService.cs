using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductService(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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

            if (product.Image != null)
            {
                string image = "Images/ProImage/";
                image += Guid.NewGuid().ToString() + " - " + product.Image.FileName;
                product.ImageUrl = "/" + image;// UI te image show kora jonno / use kore lage
                //Gide parta same name image solve korar jonno use hocche
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, image);
                product.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));//save image into folder

            }
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
