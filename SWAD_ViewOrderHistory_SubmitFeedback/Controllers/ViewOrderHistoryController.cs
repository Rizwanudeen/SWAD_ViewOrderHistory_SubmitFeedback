using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Controllers;

public class ViewOrderHistoryController
{
    private readonly List<Order> _orders;
    private readonly List<Student> _students;
    private readonly List<FoodStall> _foodStalls;

    public ViewOrderHistoryController()
    {
        // Initialize with some sample data
        _foodStalls = new List<FoodStall>
        {
            new FoodStall("S1", "Western Food Stall"),
            new FoodStall("S2", "Asian Delights")
        };

        _students = new List<Student>
        {
            new Student("U1", "John Doe", "ST1")
        };

        _orders = new List<Order>
        {
            new Order("O1", "ST1", "S1", new List<string> { "Chicken Chop", "Fries" }, DateTime.Now.AddHours(1)) 
            { 
                Status = OrderStatus.Collected,
                OrderDate = DateTime.Now.AddDays(-1)
            },
            new Order("O2", "ST1", "S2", new List<string> { "Fried Rice", "Spring Rolls" }, DateTime.Now.AddHours(2))
            {
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now
            }
        };
    }

    public List<Order> GetOrderHistory(string studentId)
    {
        return _orders
            .Where(o => o.StudentId == studentId)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
    }

    public Order? GetOrderDetails(string orderId)
    {
        return _orders.FirstOrDefault(o => o.OrderId == orderId);
    }

    public FoodStall? GetFoodStall(string stallId)
    {
        return _foodStalls.FirstOrDefault(s => s.StallId == stallId);
    }

    public bool IsOrderEligibleForFeedback(Order order)
    {
        return order.Status == OrderStatus.Collected && order.Feedback == null;
    }
}