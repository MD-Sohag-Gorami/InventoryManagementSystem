using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        #region Ctor
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService = productService;
           
        }
        #endregion

        #region Product Index Section
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
        #endregion

        #region Product Add Section
        //get
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
               
                _productService.InsertProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }
        #endregion

        #region Product Edit Section
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var editProduct = _productService.GetProductById(id.Value);

            if (editProduct == null) return NotFound();

            return View(editProduct);
        }
        //post
        [HttpPost]
        public IActionResult Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");

            }

            return View(product);
        }
        #endregion

        #region Product Delete Section with out using serives 
        /* public IActionResult Delete(int? id)
         {
             var deleteProduct = _db.Product.Find(id);
             if (deleteProduct == null) return NotFound();
             _db.Product.Remove(deleteProduct);
             _db.SaveChanges();
             return RedirectToAction("Index");
         }*/

        #endregion

        #region Prodcut Delete Section
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _productService.DeleteProduct(id.Value);

            return RedirectToAction("Index");
        }
        #endregion

        #region Product details
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var detailProduct = _productService.GetProductById(id.Value);

            if (detailProduct == null) return NotFound();

            return View(detailProduct);
        }
        #endregion



    }
}
