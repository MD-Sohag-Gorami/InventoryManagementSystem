using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Components
{
    public class TopProductsViewComponent: ViewComponent
    {
        private readonly IProductService _product;
        public TopProductsViewComponent(IProductService product)
        {
            _product = product;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var products = await _product.GetTopProductsAsync(count);
           
            return View(products);
        }
    }
}
