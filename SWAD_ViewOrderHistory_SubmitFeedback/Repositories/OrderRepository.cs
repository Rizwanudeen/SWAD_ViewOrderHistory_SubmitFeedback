using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly List<Order> _orders;

    public OrderRepository()
    {
        _orders = new List<Order>
        {
            // Original orders
            new Order("O1", "ST1", "S1", new List<string> { "Chicken Chop", "Fries" }, DateTime.Now.AddHours(1))
            {
                Status = OrderStatus.Collected,
                OrderDate = DateTime.Now.AddDays(-1)
            },
            new Order("O2", "ST1", "S2", new List<string> { "Fried Rice", "Spring Rolls" }, DateTime.Now.AddHours(2))
            {
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now
            },
            
            // New sample orders
            new Order("O3", "ST1", "S1", new List<string> { "Fish & Chips", "Coleslaw", "Soft Drink" }, DateTime.Now.AddHours(-2))
            {
                Status = OrderStatus.Collected,
                OrderDate = DateTime.Now.AddDays(-3)
            },
            
            new Order("O4", "ST1", "S2", new List<string> { "Nasi Lemak", "Extra Sambal", "Teh Tarik" }, DateTime.Now.AddHours(-1))
            {
                Status = OrderStatus.Cancelled,
                OrderDate = DateTime.Now.AddDays(-2)
            },
            
            new Order("O5", "ST1", "S1", new List<string> { "Grilled Salmon", "Mashed Potatoes", "Garden Salad" }, DateTime.Now.AddDays(-5))
            {
                Status = OrderStatus.Collected,
                OrderDate = DateTime.Now.AddDays(-5)
            },
            
            new Order("O6", "ST1", "S2", new List<string> { "Pad Thai", "Tom Yum Soup", "Thai Milk Tea" }, DateTime.Now.AddDays(-4))
            {
                Status = OrderStatus.Collected,
                OrderDate = DateTime.Now.AddDays(-4)
            },
            
            new Order("O7", "ST1", "S1", new List<string> { "Beef Burger", "Onion Rings", "Milkshake" }, DateTime.Now.AddDays(-6))
            {
                Status = OrderStatus.Collected,
                OrderDate = DateTime.Now.AddDays(-6)
            }
        };
    }

    public List<Order> GetAll() => _orders;

    public Order? GetById(string id) => _orders.FirstOrDefault(o => o.OrderId == id);

    public void Add(Order entity) => _orders.Add(entity);

    public void Update(Order entity)
    {
        var index = _orders.FindIndex(o => o.OrderId == entity.OrderId);
        if (index != -1)
        {
            _orders[index] = entity;
        }
    }

    public List<Order> GetByStudentId(string studentId) =>
        _orders.Where(o => o.StudentId == studentId).OrderByDescending(o => o.OrderDate).ToList();
}