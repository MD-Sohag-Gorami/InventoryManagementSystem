using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly IProductService _productService;
        private readonly IWareHouseService _wareHouseService;
        #region Ctor
        public ProductModelFactory(IProductService productService,
                                   IWareHouseService wareHouseService)
        {
            _productService = productService;
            _wareHouseService = wareHouseService;
        }
        #endregion
        #region Methods
        public async Task<ProductViewModel> PrepareProductViewModelAsync(ProductViewModel viewModel)
        {
            var warehouses = await _wareHouseService.GetAllWareHouseAsync();
            //viewModel.AvaiableWarehouse = warehouses.Select(x => new SelectListItem()
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //}).ToList() ;

            foreach (var warehouse in warehouses)
            {
                var item = new SelectListItem()
                {
                    Value = warehouse.Id.ToString(),
                    Text = warehouse.Name,
                };
                viewModel.AvaiableWarehouse.Add(item);
            }
            return viewModel;
        }

        public async Task< List<ProductViewModel> > PrepareAllProductsByIdAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            List<ProductViewModel> productList = new List<ProductViewModel>();

            foreach (var product in products)
            {

                /*if (product.ImageUrl != null)
                {
                    string image = "Images/ProImage/";
                    image += Guid.NewGuid().ToString() + " - " + product.ImageUrl.FileName;

                    viewModel.ImageUrl = "/" + image;

                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, image);

                    viewModel.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
                }*/

                ProductViewModel ViewModel = new ProductViewModel()
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
                };
                productList.Add(ViewModel);
            }

            if (productList == null) return new List<ProductViewModel>();
            return productList;


        }
        public async Task<ProductViewModel> PrepareProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null) return new ProductViewModel();

            ProductViewModel ViewModel = new ProductViewModel()
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
            };

            return ViewModel;

        }
        #endregion
    }
}
