using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	[Route("checkout")]
	public class CheckoutController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IOrderItemService _orderItemService;
        private readonly UserManager<IdentityUser> _userManager;
		private readonly IProductService _productService;
        public CheckoutController(IOrderService orderService, IOrderItemService orderItemService,UserManager<IdentityUser> userManager,IProductService productService)
		{
			_orderService = orderService;
			_orderItemService = orderItemService;
			_userManager = userManager;
			_productService = productService;
		}
		public IActionResult Index()
		{
			var model = new CheckOutViewModel();
            return View("Views/UI/Checkout/Index.cshtml",model);
        }
		[Route("Submit-form-cart")]
		[HttpPost]
		public async Task<JsonResult> Checkout([FromBody] CheckOutViewModel inp)
		{
			//information customer buy --order 
			var inforCustomer = inp.InformationClient;

			if (inforCustomer != null)
			{
				var order = new Order();

				order.Phone = inforCustomer.Phone;
				order.Notes = inforCustomer.Notes;
				order.PaymentMethod = "delivery_on_cash";
				order.Status = "processing";
				string address = inforCustomer.Address + " ," + inforCustomer.Ward + " ," + inforCustomer.District + " ," + inforCustomer.Province;
				order.DeliveryAddress = address;
				order.CreatedAt = DateTime.Now;
				order.UpdatedAt = DateTime.Now;
				_orderService.AddOrder(order);
				if (User.Identity.IsAuthenticated)
				{
					/*var userName = User.Identity.Name;
					var user = await _userManager.FindByEmailAsync(userName);*/

					/*Console.WriteLine(user);*/
				}
                var total = 0;
                //cart
                List<CartModelRequest> cartItems = inp.CartsModelRequest;
				if (cartItems.Count()>0)
				{
					
					foreach (var cartItem in cartItems)
					{
						var product = _productService.GetProductById(cartItem.Id);

						if (product != null)
						{
							total = (int)(total + (product.Price * cartItem.Quan));
							var orderItem = new OrderItem();
							orderItem.ProductId = product.Id;
							orderItem.OrderId = order.Id;
							orderItem.Price = product.Price;
							orderItem.Quantity = cartItem.Quan;

							_orderItemService.AddOrderItem(orderItem);

						}
					}
				}
				var orderNew = _orderService.GetOrderById(order.Id);
                orderNew.Total = total;
				_orderService.UpdateOrder(orderNew);
                return new JsonResult(true);
            }
			return new JsonResult(true);
        }
	}
}
