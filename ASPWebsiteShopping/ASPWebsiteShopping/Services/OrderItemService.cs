using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext _db;
        public OrderItemService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _db.OrderItems.ToList();
        }
        public OrderItem GetOrderItemById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var OrderItem = _db.OrderItems.FirstOrDefault(x => x.Id == id);
            return OrderItem;
        }
        public void DeleteByObj(OrderItem OrderItem)
        {
            _db.OrderItems.Remove(OrderItem);
            _db.SaveChanges();
        }
        public void UpdateOrderItem(OrderItem OrderItem)
        {
            _db.OrderItems.Update(OrderItem);
            _db.SaveChanges();
        }
        public void AddOrderItem(OrderItem OrderItem)
        {
            _db.OrderItems.Add(OrderItem);
            _db.SaveChanges();
        }
    }
}
