﻿using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class WareHouseController : Controller
    {
        #region CTor
        private readonly IWareHouseService _wareHouseService;
        public WareHouseController(IWareHouseService wareHouseService)
        {
            _wareHouseService = wareHouseService;
        }
        #endregion
        #region Methods
        public async Task<IActionResult> Index()
        {
            var wareHouse = await _wareHouseService.GetAllWareHouseAsync();
            if (wareHouse == null) View("~/Home/ProductNotFound.cshtml");

            return View(wareHouse);
        }

        [HttpGet]
        public async Task<IActionResult> AddWareHouse()
        {
            WareHouseViewModel wareHouseViewModel = new WareHouseViewModel();
            
            return View(wareHouseViewModel);
        }


        [HttpPost]      
        public async Task<IActionResult> AddWareHouse(WareHouseViewModel wareHouseViewModel)
        {
            if (ModelState.IsValid)
            {
               await _wareHouseService.InsertWareHouseAsync(wareHouseViewModel);
                return RedirectToAction("Index");
            }
            return View(wareHouseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id )
        {
            if (id == 0 || id == null) return NotFound();
            var wareHouse = await _wareHouseService.GetWareHouseByIdAsync(id.Value);
            if (wareHouse == null) return NotFound();
            return View(wareHouse);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(WareHouseViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                await _wareHouseService.UpdateWareHouseAsync(viewModel);
                return RedirectToAction("Index");

            }
            return View(viewModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == 0 || id == null) return NotFound();
            await _wareHouseService.DeleteWareHouseAsync(id);

            return RedirectToAction("Index");

        }



        #endregion


    }
}
