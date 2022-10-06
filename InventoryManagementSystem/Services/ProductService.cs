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
        private readonly IWareHouseService _wareHouseService;

        public ProductService(ApplicationDbContext db,
                              IWebHostEnvironment webHostEnvironment,
                              IWareHouseService wareHouseService)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _wareHouseService = wareHouseService;
        }
        #endregion
        #region Methods
        public async Task<List<ProductModel>> GetAllProductsAsync(string productSearch = "", int warehouseId=0)
        {
            var products = _db.Product.ToList();
            if (products == null) return new List<ProductModel>();

            if (!String.IsNullOrEmpty(productSearch))
            {
                products = products.Where(product => product.Name.Contains(productSearch)).ToList();
            }
            if(warehouseId > 0)
            {
                products = products.Where(product => product.WareHouseId == warehouseId).ToList();
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

        public async Task<ProductViewModel> GetProductDetailByIdAsync(int id)
        {
            var product = await _db.Product.FindAsync(id);
            if (product == null) return new ProductViewModel();
            //var warehouse = await _db.WareHouse.FindAsync(product.WareHouseId);
            var warehouse = await _wareHouseService.GetWareHouseByIdAsync(product.WareHouseId);
            var ViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SellPrice = product.SellPrice,
                ProductQnty = product.ProductQnty,
                PurchasePrice = product.PurchasePrice,
                WareHouseId = product.WareHouseId,
                CreateDateOn = product.CreateDateOn,
                ImageUrl = product.ImageUrl,
                /*  WareHouseName = warehouse?.Name ?? string.Empty,*/
                WareHouseName = warehouse?.Name,
            };

            return ViewModel;
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
