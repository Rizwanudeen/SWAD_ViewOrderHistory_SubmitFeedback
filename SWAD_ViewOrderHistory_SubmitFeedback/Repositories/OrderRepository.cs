using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

public class OrderRepository
{
    private readonly List<Order> _orders;

    public OrderRepository()
    {
        _orders = new List<Order>
        {
            new Order("O1", DateTime.Now.AddDays(-1), "Collected", DateTime.Now.AddHours(1), "QR1", false),
            new Order("O2", DateTime.Now, "Pending", DateTime.Now.AddHours(2), "QR2", true),
            new Order("O3", DateTime.Now.AddDays(-3), "Cancelled", DateTime.Now.AddHours(-2), "QR3", false),
            new Order("O4", DateTime.Now.AddDays(-2), "Cancelled", DateTime.Now.AddHours(-1), "QR4", false),
            new Order("O5", DateTime.Now.AddDays(-5), "Collected", DateTime.Now.AddDays(-5), "QR5", false),
            new Order("O6", DateTime.Now.AddDays(-4), "Collected", DateTime.Now.AddDays(-4), "QR6", false),
            new Order("O7", DateTime.Now.AddDays(-6), "Collected", DateTime.Now.AddDays(-6), "QR7", false)
        };
    }

    public List<Order> GetAll() => _orders;

    public Order? GetById(string id) => _orders.FirstOrDefault(o => o.OrderId == id);

    public void Add(Order order) => _orders.Add(order);

    public void Update(Order order)
    {
        var index = _orders.FindIndex(o => o.OrderId == order.OrderId);
        if (index != -1)
        {
            _orders[index] = order;
        }
    }

    public void Delete(string id)
    {
        var order = GetById(id);
        if (order != null)
        {
            _orders.Remove(order);
        }
    }
}