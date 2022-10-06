using InventoryManagementSystem.Factories;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class WareHouseController : Controller
    {
        #region Ctor
        private readonly IWareHouseService _wareHouseService;
        private readonly IWarehouseModelFactory _warehouseModelFactory;

        public WareHouseController(IWareHouseService wareHouseService,
                                   IWarehouseModelFactory warehouseModelFactory)
        {
            _wareHouseService = wareHouseService;
            _warehouseModelFactory = warehouseModelFactory;
        }
        #endregion
        #region Methods
        public async Task<IActionResult> Index(int pg = 1 , string warehouseSearch="")
        {
            var wareHouses = await _warehouseModelFactory.PrepareAllWareHoueAsync(warehouseSearch);
            const int pageSize = 2;
            if (pg < 1) pg = 1;
            int recsCount = wareHouses.Count;
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = wareHouses.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }
        [Authorize(Roles ="Manager")]
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
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id )
        {
            if (id == 0 || id == null) return NotFound();
            var wareHouse = await _warehouseModelFactory.PrepareWareHouseByIdAsync(id.Value);

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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == 0 || id == null) return NotFound();
            await _wareHouseService.DeleteWareHouseAsync(id);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var detailWarehouse = await _warehouseModelFactory.PrepareWareHouseByIdAsync(id.Value);

            if (detailWarehouse == null) return NotFound();

            return View(detailWarehouse);
        }


        #endregion


    }
}
