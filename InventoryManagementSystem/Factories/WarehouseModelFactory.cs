using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Factories
{
    public class WarehouseModelFactory : IWarehouseModelFactory
    {
        private readonly IProductService _iproducService;
        private readonly IWareHouseService _wareHouseService;

        public WarehouseModelFactory(IProductService iproducService,
                                    IWareHouseService wareHouseService)
        {
            _iproducService = iproducService;
            _wareHouseService = wareHouseService;
        }
        private async Task<WareHouseViewModel> CreateWareHoueAsync(WareHouseModel model)
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

            };
            return ViewModel;
        }
        public async Task<List<WareHouseViewModel>> PrepareAllWareHoueAsync()
        {
            var model = await _wareHouseService.GetAllWareHouseAsync();

            List<WareHouseViewModel> wareHouses = new List<WareHouseViewModel>();
            foreach (var item in model)
            {
                var createView = await CreateWareHoueAsync(item);
                wareHouses.Add(createView);
            }

            return wareHouses;
        }
        public async Task<WareHouseViewModel> PrepareWareHouseByIdAsync(int id)
        {
            var model = await _wareHouseService.GetWareHouseByIdAsync(id);

            var createView = await CreateWareHoueAsync(model);

            return createView;
        }


    }
}
