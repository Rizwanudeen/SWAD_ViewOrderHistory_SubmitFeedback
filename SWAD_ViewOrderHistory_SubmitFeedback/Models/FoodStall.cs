namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class FoodStall
{
    public string StallId { get; set; }
    public string StallName { get; set; }
    public List<Order> Orders { get; set; } = new();
    public List<Feedback> Feedbacks { get; set; } = new();

    public FoodStall(string stallId, string stallName)
    {
        StallId = stallId;
        StallName = stallName;
    }
}