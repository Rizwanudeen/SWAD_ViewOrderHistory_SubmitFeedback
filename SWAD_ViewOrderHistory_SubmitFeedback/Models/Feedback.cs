namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class Feedback
{
    public string FeedbackId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsFlagged { get; set; }
    public string OrderId { get; set; }
    public string StudentId { get; set; }
    public string StallId { get; set; }
    public StaffResponse? Response { get; set; }

    public Feedback(string feedbackId, string content, string orderId, string studentId, string stallId)
    {
        FeedbackId = feedbackId;
        Content = content;
        OrderId = orderId;
        StudentId = studentId;
        StallId = stallId;
        Timestamp = DateTime.Now;
        IsFlagged = false;
    }
}