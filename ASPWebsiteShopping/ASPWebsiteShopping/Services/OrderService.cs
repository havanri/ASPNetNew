using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPWebsiteShopping.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _db.Orders.Where(e => e.DeletedAt == null).Include(x=>x.OrderItems).ThenInclude(p=>p.Product);
        }
        public Order GetOrderById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var Order = _db.Orders.Where(e => e.DeletedAt == null).Include(x => x.OrderItems).FirstOrDefault(x => x.Id == id);
            return Order;
        }
        public void DeleteByObj(Order Order)
        {
            _db.Orders.Remove(Order);
            _db.SaveChanges();
        }
        public void UpdateOrder(Order Order)
        {
            _db.Orders.Update(Order);
            _db.SaveChanges();
        }
        public void AddOrder(Order Order)
        {
            _db.Orders.Add(Order);
            _db.SaveChanges();
        }
    }
}
