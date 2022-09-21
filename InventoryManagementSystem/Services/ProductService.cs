using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProductService
    {
        #region Ctor
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
      
        public ProductService(ApplicationDbContext db,
                              IWebHostEnvironment webHostEnvironment )
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
          
        }
        #endregion
        #region Methods
        public async Task<List<ProductModel>> GetAllProductsAsync(string productSearch = "")
        {
            var products = _db.Product.ToList();
            if (products == null) return new List<ProductModel>();

            if (!String.IsNullOrEmpty(productSearch))
            {
                products = products.Where(product => product.Name.Contains(productSearch)).ToList();
            }

            return products;
        }
        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var product = await _db.Product.FindAsync(id);
          
            if (product == null)
            {
                return new ProductModel();
            }
            return product;
        }
      
        public async Task UpdateProductAsync(ProductViewModel viewModel)
        {

            var productModel = await _db.Product.FindAsync(viewModel.Id);
            if (productModel == null) return;
            productModel.Id = viewModel.Id;
            productModel.Name = viewModel.Name;
            productModel.Description = viewModel.Description;
            productModel.CreateDateOn = viewModel.CreateDateOn;
            productModel.ProductQnty = viewModel.ProductQnty;
            productModel.SellPrice = viewModel.SellPrice;
            productModel.PurchasePrice = viewModel.PurchasePrice;
            productModel.WareHouseId = viewModel.WareHouseId;

            if (viewModel.Image != null)
            {
                string image = "Images/ProImage/";
                image += Guid.NewGuid().ToString() + " - " + viewModel.Image.FileName;

                viewModel.ImageUrl = "/" + image;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, image);

                viewModel.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
         
            _db.Product.Update(productModel);
            await _db.SaveChangesAsync();
        }
     
        public async Task DeleteProductAsync(int id)
        {
            var product =  await GetProductByIdAsync(id);

            var filePath = product.ImageUrl;
            filePath = filePath.Substring(1);
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
            if (File.Exists(serverFolder))
            {
                File.Delete(serverFolder);
            }

            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
        }
      
        public async Task InsertProductAsync(ProductViewModel viewModel)
        {

            ProductModel productModel = new ProductModel();
            productModel.Id = viewModel.Id;
            productModel.Name = viewModel.Name;
            productModel.Description = viewModel.Description;
            productModel.CreateDateOn = viewModel.CreateDateOn;
            productModel.ProductQnty = viewModel.ProductQnty;
            productModel.SellPrice = viewModel.SellPrice;
            productModel.PurchasePrice = viewModel.PurchasePrice;
            productModel.WareHouseId = viewModel.WareHouseId;
            if (viewModel.Image != null)
            {
                string image = "Images/ProImage/";
                image += Guid.NewGuid().ToString() + " - " + viewModel.Image.FileName;

                viewModel.ImageUrl = "/" + image;
              
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, image);

                viewModel.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
            await _db.Product.AddAsync(productModel);
            await _db.SaveChangesAsync();
        }

        #endregion


    }
}
