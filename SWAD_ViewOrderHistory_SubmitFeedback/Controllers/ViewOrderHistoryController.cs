using System;
using System.Collections.Generic;
using System.Linq;
using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Controllers;

public class ViewOrderHistoryController
{
    private readonly List<Order> _orders;
    private readonly List<Student> _students;
    private readonly List<FoodStall> _foodStalls;

    public ViewOrderHistoryController()
    {
        // Initialize with sample data
        _foodStalls = new List<FoodStall>
        {
            new FoodStall("S1", "Western Food Stall", "8am-8pm", 15),
            new FoodStall("S2", "Asian Delights", "9am-9pm", 10)
        };

        _students = new List<Student>
        {
            new Student("U1", "John Doe", "password123", 0, false, "Regular", null)
        };

        _orders = new List<Order>
        {
            new Order("O1", DateTime.Now.AddDays(-1), "Collected", DateTime.Now.AddHours(1), "QR1", false),
            new Order("O2", DateTime.Now, "Pending", DateTime.Now.AddHours(2), "QR2", false)
        };
    }

    public List<Order> GetOrderHistory(string studentId)
    {
        return _orders.OrderByDescending(o => o.OrderDateTime).ToList();
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
        return order.Status == "Collected";
    }
}