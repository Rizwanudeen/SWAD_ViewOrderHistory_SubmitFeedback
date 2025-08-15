using SWAD_ViewOrderHistory_SubmitFeedback.Models;
using SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Services;

public class FeedbackService
{
    private readonly FeedbackRepository _feedbackRepository;
    private readonly OrderService _orderService;

    public FeedbackService(FeedbackRepository feedbackRepository, OrderService orderService)
    {
        _feedbackRepository = feedbackRepository;
        _orderService = orderService;
    }

    public (bool success, string message, Feedback? feedback) SubmitFeedback(string orderId, string studentId, string content)
    {
        var order = _orderService.GetOrderById(orderId);
        if (order == null)
        {
            return (false, "Order not found.", null);
        }

        if (!_orderService.IsOrderEligibleForFeedback(order))
        {
            return (false, "Order is not eligible for feedback.", null);
        }

        if (order.Feedback != null)
        {
            return (false, "Feedback already exists for this order.", null);
        }

        if (string.IsNullOrWhiteSpace(content) || content.Length < 10)
        {
            return (false, "Invalid feedback. Feedback must not be empty and must be at least 10 characters.", null);
        }

        var feedback = new Feedback(Guid.NewGuid().ToString(), content);
        _feedbackRepository.Add(feedback);

        // Associate feedback with the order
        order.Feedback = feedback;
        _orderService.UpdateOrder(order);

        return (true, "Feedback submitted successfully.", feedback);
    }
}