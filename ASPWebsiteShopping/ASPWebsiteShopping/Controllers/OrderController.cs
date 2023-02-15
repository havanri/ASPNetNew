
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
        public class OrderController : Controller
        {
            private readonly IOrderService _orderService;
        private readonly IProductService _productService;


            public OrderController(IOrderService orderService,IProductService productService)
            {
                _orderService = orderService;
                _productService = productService;
            }

            public IActionResult Index()
            {
                var model = new OrderViewModel();
                model.Orders = _orderService.GetAllOrders().ToList();
                return View("Views/Admin/Order/Index.cshtml", model);
            }
            public IActionResult Show(int? id)
            {
            //
            var model = new OrderViewModel();
            var order = _orderService.GetOrderById(id);
            //thong tin hoa don chi tiet
            model.Order = order;

            ////Danh sachs sanr pham
            ///
            List<Product> productOfOrder = new List<Product>();
            model.OrderItems = order.OrderItems;
            foreach(var item in order.OrderItems)
            {
                var product = _productService.GetProductById(item.ProductId);
                productOfOrder.Add(product);
            }
            model.Products = productOfOrder;
            return View("Views/Admin/Order/OrderDetail.cshtml", model);
            }
           
           
        }
    }
