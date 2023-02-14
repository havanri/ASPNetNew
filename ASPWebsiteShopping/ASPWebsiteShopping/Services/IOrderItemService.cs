using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
	public interface IOrderItemService
	{
		void AddOrderItem(OrderItem OrderItem);
		void DeleteByObj(OrderItem OrderItem);
		IEnumerable<OrderItem> GetAllOrderItems();
		OrderItem GetOrderItemById(int? id);
		void UpdateOrderItem(OrderItem OrderItem);
	}
}