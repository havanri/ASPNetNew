using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
	public interface IOrderService
	{
		void AddOrder(Order Order);
		void DeleteByObj(Order Order);
		IEnumerable<Order> GetAllOrders();
		Order GetOrderById(int? id);
		void UpdateOrder(Order Order);
	}
}