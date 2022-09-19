using InventoryManagementSystem.Data;
using InventoryManagementSystem.Factories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        #region Ctor
        private readonly IProductService _productService;
        private readonly IProductModelFactory _productModelFactory;

       public ProductController(IProductService productService,
                                 IProductModelFactory productModelFactory)
        {
            _productService = productService;
            _productModelFactory = productModelFactory;
        }
        #endregion

        #region Methods
      
        public async Task <IActionResult> Index()
        {
            var products =await _productModelFactory.PrepareAllProductsByIdAsync();
            if (products == null) return View("~/Home/ProductNotFound.cshtml");
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var viewModel = await _productModelFactory.PrepareProductViewModelAsync(new ProductViewModel());
            return View(viewModel);
        }
        [HttpPost]
        public async Task <IActionResult> AddProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
   
               await _productService.InsertProductAsync(productViewModel);
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var editProduct = await _productModelFactory.PrepareProductByIdAsync(id.Value);

            if (editProduct == null) return NotFound();

            return View(editProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
               await _productService.UpdateProductAsync(productViewModel);
                return RedirectToAction("Index");

            }

            return View(productViewModel);
        }

        /* public IActionResult Delete(int? id)
         {
             var deleteProduct = _db.Product.Find(id);
             if (deleteProduct == null) return NotFound();
             _db.Product.Remove(deleteProduct);
             _db.SaveChanges();
             return RedirectToAction("Index");
         }*/

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View("~/Home/Product/ProductNotFound.cshtml");
            }
           await _productService.DeleteProductAsync(id.Value);

            return RedirectToAction("Index");
        }
     
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var detailProduct = await _productService.GetProductByIdAsync(id.Value);

            if (detailProduct == null) return NotFound();

            return View(detailProduct);
        }
        
        #endregion


    }
}
