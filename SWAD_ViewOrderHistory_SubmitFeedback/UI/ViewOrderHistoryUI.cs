using SWAD_ViewOrderHistory_SubmitFeedback.Models;
using SWAD_ViewOrderHistory_SubmitFeedback.Services;

namespace SWAD_ViewOrderHistory_SubmitFeedback.UI;

public class ViewOrderHistoryUI
{
    private readonly OrderService _orderService;
    private readonly FeedbackService _feedbackService;
    private const int MinFeedbackLength = 10;

    public ViewOrderHistoryUI(OrderService orderService, FeedbackService feedbackService)
    {
        _orderService = orderService;
        _feedbackService = feedbackService;
    }

    private void ClearScreen()
    {
        try { Console.Clear(); }
        catch { Console.Write("\x1b[2J\x1b[H"); }
    }

    public void DisplayNoOrderHistory()
    {
        Console.WriteLine("No past orders found.");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(true);
        while (Console.KeyAvailable) Console.ReadKey(true);
    }

    private void DisplayOrder(Order order, int index)
    {
        var stall = _orderService.GetFoodStall(order.StallId);
        Console.WriteLine($"{index}. Order {order.OrderId} - {stall?.StallName}");
        Console.WriteLine($"   Items: {string.Join(", ", order.Items)}");
        Console.WriteLine($"   Status: {order.Status}");
        Console.WriteLine($"   Date: {order.OrderDate:g}");
        Console.WriteLine($"   Pickup Time: {order.PickupTime:g}");
        if (order.Feedback != null)
            Console.WriteLine($"   Feedback: Submitted on {order.Feedback.Timestamp:g}");
        else if (_orderService.IsOrderEligibleForFeedback(order))
            Console.WriteLine("   Feedback: Eligible for feedback");
        else
            Console.WriteLine("   Feedback: Not eligible yet");
        Console.WriteLine();
    }

    // Robust: keeps asking until user enters a number between 0..orders.Count
    public int DisplayOrderHistory(List<Order> orders)
    {
        ClearScreen();
        Console.WriteLine("=== Order History ===\n");
        if (orders.Count == 0)
        {
            DisplayNoOrderHistory();
            return 0;
        }

        for (int i = 0; i < orders.Count; i++)
            DisplayOrder(orders[i], i + 1);

        while (true)
        {
            Console.Write($"\nSelect an order number (1-{orders.Count}) or 0 to exit: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int selection))
            {
                if (selection == 0) return 0;
                if (selection >= 1 && selection <= orders.Count) return selection;
            }

            Console.WriteLine($"Invalid selection. Please enter a number between 0 and {orders.Count}.");
        }
    }

    public void DisplayOrderDetails(Order order)
    {
        ClearScreen();
        Console.WriteLine($"=== Order Details: {order.OrderId} ===\n");
        var stall = _orderService.GetFoodStall(order.StallId);
        Console.WriteLine($"Stall: {stall?.StallName}");
        Console.WriteLine($"Items: {string.Join(", ", order.Items)}");
        Console.WriteLine($"Status: {order.Status}");
        Console.WriteLine($"Order Date: {order.OrderDate:g}");
        Console.WriteLine($"Pickup Time: {order.PickupTime:g}");
    }

    // Returns true if user wants to submit feedback; false to go back
    public bool DisplayActions(Order order)
    {
        if (_orderService.IsOrderEligibleForFeedback(order))
        {
            Console.WriteLine("\nThis order is eligible for feedback!");
            while (true)
            {
                Console.Write("Would you like to submit feedback? (Y/N): ");
                var ans = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
                if (ans == "Y") return true;
                if (ans == "N") return false;
                Console.WriteLine("Please type Y or N.");
            }
        }
        else if (order.Feedback != null)
        {
            DisplayExistingFeedback(order.Feedback);
            Console.WriteLine("\nPress any key to return to order history...");
            Console.ReadKey(true);
            while (Console.KeyAvailable) Console.ReadKey(true);
            return false;
        }
        else
        {
            Console.WriteLine("\nThis order is not eligible for feedback yet.");
            Console.WriteLine("Only completed orders are eligible for feedback.");
            Console.WriteLine("\nPress any key to return to order history...");
            Console.ReadKey(true);
            while (Console.KeyAvailable) Console.ReadKey(true);
            return false;
        }
    }

    // Loops until feedback is long enough, or user cancels with a single 0
    public string? PromptFeedback()
    {
        while (true)
        {
            ClearScreen();
            Console.WriteLine("=== Submit Feedback ===\n");
            Console.WriteLine($"Please enter your feedback (minimum {MinFeedbackLength} characters).");
            Console.WriteLine("Enter a single 0 to cancel.\n");
            Console.Write("> ");

            string text = (Console.ReadLine() ?? "").Trim();

            if (text == "0") return null;
            if (text.Length >= MinFeedbackLength) return text;

            Console.WriteLine($"\nToo short ({text.Length}/{MinFeedbackLength}). Press any key to try again...");
            Console.ReadKey(true);
            while (Console.KeyAvailable) Console.ReadKey(true);
        }
    }

    public void DisplayFeedbackSubmissionSuccess(Feedback feedback)
    {
        ClearScreen();
        Console.WriteLine("=== Feedback Submitted Successfully! ===");
        Console.WriteLine($"Feedback ID: {feedback.FeedbackId}");
        Console.WriteLine($"Timestamp: {feedback.Timestamp:g}");
        Console.WriteLine($"Content: {feedback.Content}");
        Console.WriteLine("\nYour feedback has been stored and will be reviewed by the stall staff.");
        Console.WriteLine("Thank you for helping us improve our service!");
        Console.WriteLine("\nPress any key to return to order history...");
        Console.ReadKey(true);
        while (Console.KeyAvailable) Console.ReadKey(true);
    }

    private void DisplayExistingFeedback(Feedback feedback)
    {
        Console.WriteLine("\n=== Submitted Feedback ===");
        Console.WriteLine($"Submitted on: {feedback.Timestamp:g}");
        Console.WriteLine("Your feedback:");
        Console.WriteLine(feedback.Content);
        if (feedback.Response != null)
        {
            Console.WriteLine("\nStall Response:");
            Console.WriteLine($"Responded on: {feedback.Response.Timestamp:g}");
            Console.WriteLine(feedback.Response.Content);
        }
    }

    public void DisplayValidationError(string message)
    {
        ClearScreen();
        Console.WriteLine("=== Error ===");
        Console.WriteLine(message);
        Console.WriteLine("\nPress any key to try again...");
        Console.ReadKey(true);
        while (Console.KeyAvailable) Console.ReadKey(true);
    }
}
