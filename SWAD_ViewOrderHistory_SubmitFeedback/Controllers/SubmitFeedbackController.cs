using System;
using System.Collections.Generic;
using System.Linq;
using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Controllers;

public class SubmitFeedbackController
{
    private readonly List<Feedback> _feedbacks = new();
    private readonly ViewOrderHistoryController _orderHistoryController;

    public SubmitFeedbackController(ViewOrderHistoryController orderHistoryController)
    {
        _orderHistoryController = orderHistoryController;
    }

    public bool HasExistingFeedback(string orderId)
    {
        var order = _orderHistoryController.GetOrderDetails(orderId);
        return order?.Feedback != null;
    }

    public bool ValidateFeedback(string feedback)
    {
        return !string.IsNullOrWhiteSpace(feedback) && feedback.Length >= 10;
    }

    public Feedback? SubmitFeedback(string orderId, string studentId, string content)
    {
        var order = _orderHistoryController.GetOrderDetails(orderId);
        if (order == null || !_orderHistoryController.IsOrderEligibleForFeedback(order))
        {
            return null;
        }

        if (HasExistingFeedback(orderId))
        {
            Console.WriteLine("\n=== Submitted Feedback ===");
            Console.WriteLine($"Submitted on: {order.Feedback?.Timestamp:g}");
            Console.WriteLine("Your feedback:");
            Console.WriteLine(order.Feedback?.Content);
            Console.WriteLine("\nPress any key to return to order history...");
            Console.ReadKey(true);
            return null;
        }

        if (!ValidateFeedback(content))
        {
            return null;
        }

        var feedback = new Feedback(
            Guid.NewGuid().ToString(),
            content
        );

        _feedbacks.Add(feedback);
        order.Feedback = feedback;
        return feedback;
    }
}