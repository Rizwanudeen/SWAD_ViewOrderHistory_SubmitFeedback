namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class StaffResponse
{
    public string ResponseId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public string StaffId { get; set; }
    public string FeedbackId { get; set; }

    public StaffResponse(string responseId, string content, string staffId, string feedbackId)
    {
        ResponseId = responseId;
        Content = content;
        StaffId = staffId;
        FeedbackId = feedbackId;
        Timestamp = DateTime.Now;
    }
}