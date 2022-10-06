﻿using InventoryManagementSystem.Data;
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
        public WareHouseService(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment
                                 )
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
          
        }
        #endregion
        #region Methods
        public async Task<List<WareHouseModel>> GetAllWareHouseAsync(string warehouseSearch="")
        {
            var warehouses = await _db.WareHouse.ToListAsync();
            if (warehouses == null) return new List<WareHouseModel> ();

            if (!String.IsNullOrEmpty(warehouseSearch))
            {
                warehouses = warehouses.Where(warehouse => warehouse.Name.Contains(warehouseSearch)).ToList();
            }
            return warehouses;
        }
        public async Task<WareHouseModel> GetWareHouseByIdAsync(int? id)
        {
            var model = await _db.WareHouse.FindAsync(id);

            if (model == null) return new WareHouseModel();

            return model;
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

       /* public async Task<WareHouseViewModel> GetWareHouseDetailByIdAsync(int id)
        {
            var model = await _db.WareHouse.FindAsync(id);
            if (model == null) return new WareHouseViewModel();

            model.ProductList = await _productService.GetAllProductsAsync(warehouseId: id);
           
            var ViewModel = new WareHouseViewModel()
            {
               Id = id,
               Name = model.Name,
               Location = model.Location,
                ProductList = model.ProductList.Select(m => new ProductViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,


                }).ToList(),

            };
         
            return ViewModel;
        }*/
        #endregion

    }
}
