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
        return _feedbacks.Any(f => f.OrderId == orderId);
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
            return null;
        }

        if (!ValidateFeedback(content))
        {
            return null;
        }

        var feedback = new Feedback(
            $"F{_feedbacks.Count + 1}",
            content,
            orderId,
            studentId,
            order.StallId
        );

        _feedbacks.Add(feedback);
        order.Feedback = feedback;
        return feedback;
    }
}