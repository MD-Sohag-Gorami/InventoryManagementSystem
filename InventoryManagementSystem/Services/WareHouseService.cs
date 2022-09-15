using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #region CTor
        public WareHouseService(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Methods
        public async Task<List<WareHouseViewModel>> GetAllWareHouseAsync()
        {
            var model = await _db.WareHouse.ToListAsync();

            List<WareHouseViewModel> wareHouses = new List<WareHouseViewModel>();
            foreach (var item in model)
            {
                string byteImage = null;
                string byteImageData = null;
                if (item.ImgByte != null)
                {
                     byteImage = Convert.ToBase64String(item.ImgByte);
                     byteImageData = String.Format("data:image/gif;base64,{0}", byteImage);
                }

                WareHouseViewModel ViewModel = new WareHouseViewModel()
                { Id = item.Id,
                  ImgByteUrl = byteImageData,
                  ImgByte = null,
                  Name = item.Name,
                  Location = item.Location,

                };
                wareHouses.Add(ViewModel);
            }
            if (wareHouses == null) return new List<WareHouseViewModel> ();
            return wareHouses;
        }
        public async Task<WareHouseViewModel> GetWareHouseByIdAsync(int? id)
        {
            var model = await _db.WareHouse.FindAsync(id.Value);

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
        public async Task UpdateWareHouseAsync(WareHouseViewModel viewModel)
        {
            var wareHouse = await _db.WareHouse.FindAsync(viewModel.Id);
            if(wareHouse == null) return;
            wareHouse.Id = viewModel.Id;
            wareHouse.Name = viewModel.Name;
            wareHouse.Location = viewModel.Location;
            if (viewModel.ImgByte != null)
            {
                var stream = new MemoryStream();
                await viewModel.ImgByte.CopyToAsync(stream);

                wareHouse.ImgByte = stream.ToArray();
            }
          
             _db.WareHouse.Update(wareHouse);
            await _db.SaveChangesAsync();

        }

        public async Task InsertWareHouseAsync(WareHouseViewModel viewModel)
        {
            WareHouseModel wareHouse = new WareHouseModel();
            wareHouse.Id = viewModel.Id;
            wareHouse.Name = viewModel.Name;
            wareHouse.Location = viewModel.Location;
            if(viewModel.ImgByte != null)
            {
                var stream = new MemoryStream();
                await viewModel.ImgByte.CopyToAsync(stream);

                wareHouse.ImgByte = stream.ToArray();
            }
            await _db.WareHouse.AddAsync(wareHouse);
            await _db.SaveChangesAsync();

        }
        public async Task DeleteWareHouseAsync(int? id)
        {
            if (id == null) return;
            var wareHouse = await _db.WareHouse.FindAsync(id);
            if (wareHouse == null) return;
            _db.Remove(wareHouse);
            await _db.SaveChangesAsync();

        }
        #endregion

    }
}
