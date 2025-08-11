using SWAD_ViewOrderHistory_SubmitFeedback.Models;
using SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Services;

public class FeedbackService
{
    private readonly FeedbackRepository _feedbackRepository;
    private readonly OrderService _orderService;

    public event Action<Feedback>? FeedbackSubmitted;

    public FeedbackService(FeedbackRepository feedbackRepository, OrderService orderService)
    {
        _feedbackRepository = feedbackRepository;
        _orderService = orderService;
    }

    public bool HasExistingFeedback(string orderId)
    {
        return _feedbackRepository.ExistsForOrder(orderId);
    }

    public bool ValidateFeedback(string feedback)
    {
        return !string.IsNullOrWhiteSpace(feedback) && feedback.Length >= 10;
    }

    public (bool success, string message, Feedback? feedback) SubmitFeedback(string orderId, string studentId, string content)
    {
        var order = _orderService.GetOrderDetails(orderId);
        if (order == null)
        {
            return (false, "Order not found.", null);
        }

        if (!_orderService.IsOrderEligibleForFeedback(order))
        {
            return (false, "Order is not eligible for feedback.", null);
        }

        if (HasExistingFeedback(orderId))
        {
            return (false, "Feedback already exists for this order.", null);
        }

        if (!ValidateFeedback(content))
        {
            return (false, "Feedback must be at least 10 characters long.", null);
        }

        var feedback = new Feedback(
            $"F{Guid.NewGuid().ToString("N").Substring(0, 8)}",
            content,
            orderId,
            studentId,
            order.StallId
        );

        _feedbackRepository.Add(feedback);
        order.Feedback = feedback;
        _orderService.UpdateOrder(order);

        FeedbackSubmitted?.Invoke(feedback);

        return (true, "Thank you for your feedback!", feedback);
    }
}