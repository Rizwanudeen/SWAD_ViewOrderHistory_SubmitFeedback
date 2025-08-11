using SWAD_ViewOrderHistory_SubmitFeedback.Repositories;
using SWAD_ViewOrderHistory_SubmitFeedback.Services;
using SWAD_ViewOrderHistory_SubmitFeedback.UI;

namespace SWAD_ViewOrderHistory_SubmitFeedback;

class Program
{
    static void Main(string[] args)
    {
        // Setup repositories
        var orderRepository = new OrderRepository();
        var foodStallRepository = new FoodStallRepository();
        var feedbackRepository = new FeedbackRepository();

        // Setup services
        var orderService = new OrderService(orderRepository, foodStallRepository);
        var feedbackService = new FeedbackService(feedbackRepository, orderService);

        // Setup UI
        var viewOrderHistoryUI = new ViewOrderHistoryUI(orderService, feedbackService);

        // Simulating a logged-in student with ID "ST1"
        string studentId = "ST1";

        while (true)
        {
            var orders = orderService.GetOrderHistory(studentId);
            int selection = viewOrderHistoryUI.DisplayOrderHistory(orders);
            if (selection == 0) break; // exit

            var selectedOrder = orders[selection - 1];

            viewOrderHistoryUI.DisplayOrderDetails(selectedOrder);
            bool wantsToSubmit = viewOrderHistoryUI.DisplayActions(selectedOrder);

            if (wantsToSubmit)
            {
                var content = viewOrderHistoryUI.PromptFeedback();
                if (content == null)
                {
                    // user cancelled feedback; go back to list
                    continue;
                }

                var (success, message, feedback) = feedbackService.SubmitFeedback(
                    selectedOrder.OrderId, studentId, content);

                if (success && feedback != null)
                    viewOrderHistoryUI.DisplayFeedbackSubmissionSuccess(feedback);
                else
                    viewOrderHistoryUI.DisplayValidationError(message);
            }
        }
    }
}
