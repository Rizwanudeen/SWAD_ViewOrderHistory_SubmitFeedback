namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class FoodStall
{
    public string StallId { get; set; }
    public string StallName { get; set; }
    public List<string> Permissions { get; set; } = new();
    public string OperationHours { get; set; }
    public int AveragePrepTime { get; set; }

    public FoodStall(string stallId, string stallName, string operationHours, int averagePrepTime)
    {
        StallId = stallId;
        StallName = stallName;
        OperationHours = operationHours;
        AveragePrepTime = averagePrepTime;
    }
}