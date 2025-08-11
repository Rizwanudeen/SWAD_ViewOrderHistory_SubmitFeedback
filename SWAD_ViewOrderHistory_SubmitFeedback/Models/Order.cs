namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class Order
{
    public string OrderId { get; set; }
    public string StudentId { get; set; }
    public string StallId { get; set; }
    public List<string> Items { get; set; }
    public DateTime PickupTime { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public Feedback? Feedback { get; set; }

    public Order(string orderId, string studentId, string stallId, List<string> items, DateTime pickupTime)
    {
        OrderId = orderId;
        StudentId = studentId;
        StallId = stallId;
        Items = items;
        PickupTime = pickupTime;
        OrderDate = DateTime.Now;
        Status = OrderStatus.Pending;
    }
}

public enum OrderStatus
{
    Pending,
    Collected,
    Cancelled
}