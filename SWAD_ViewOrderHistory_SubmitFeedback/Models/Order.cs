namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class Order
{
    public string OrderId { get; set; }
    public DateTime OrderDateTime { get; set; }
    public string Status { get; set; }
    public DateTime PickupTime { get; set; }
    public string QrCode { get; set; }
    public Feedback? Feedback { get; set; }
    public bool isBeingPrepared { get; set; }

    public Order(string orderId, DateTime orderDateTime, string status, DateTime pickupTime, string qrCode, bool isBeingPrepared)
    {
        OrderId = orderId;
        OrderDateTime = orderDateTime;
        Status = status;
        PickupTime = pickupTime;
        QrCode = qrCode;
        this.isBeingPrepared = isBeingPrepared;
    }
}