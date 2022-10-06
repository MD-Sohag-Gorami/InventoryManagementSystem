using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public class WarehouseModelFactory : IWarehouseModelFactory
    {
        private readonly IProductService _productService;
        private readonly IWareHouseService _wareHouseService;

        public WarehouseModelFactory(IProductService productService,
                                    IWareHouseService wareHouseService)
        {
            _productService = productService;
            _wareHouseService = wareHouseService;
        }
        private async Task<WareHouseViewModel> PrepareWareHoueByModelAsync(WareHouseModel model)
        {
            string byteImage = null;
            string byteImageData = null;
            if (model.ImgByte != null)
            {
                byteImage = Convert.ToBase64String(model.ImgByte);
                byteImageData = String.Format("data:image/gif;base64,{0}", byteImage);
            }

            WareHouseViewModel ViewModel = new WareHouseViewModel()
            {
                Id = model.Id,
                ImgByteUrl = byteImageData,
                ImgByte = null,
                Name = model.Name,
                Location = model.Location,
                ProductList = model.ProductList.Select(m => new ProductViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    PurchasePrice = m.PurchasePrice,
                    SellPrice = m.SellPrice,
                    CreateDateOn = m.CreateDateOn,
                    ProductQnty = m.ProductQnty,
                    ImageUrl = m.ImageUrl,


                }).ToList(),

            };
            return ViewModel;
        }
        public async Task<List<WareHouseViewModel>> PrepareAllWareHoueAsync(string warehouseSearch = "")
        {
            var model = await _wareHouseService.GetAllWareHouseAsync(warehouseSearch);

            List<WareHouseViewModel> wareHouses = new List<WareHouseViewModel>();
            foreach (var item in model)
            {
                var createView = await PrepareWareHoueByModelAsync(item);
                wareHouses.Add(createView);
            }

            return wareHouses;
        }
        public async Task<WareHouseViewModel> PrepareWareHouseByIdAsync(int id)
        {
            var model = await _wareHouseService.GetWareHouseByIdAsync(id);
            model.ProductList = await _productService.GetAllProductsAsync(warehouseId: id);

            var createView = await PrepareWareHoueByModelAsync(model);

            return createView;
        }


    }
}
