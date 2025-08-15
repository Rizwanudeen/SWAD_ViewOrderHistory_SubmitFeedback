namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class Feedback
{
    public string FeedbackId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsFlagged { get; set; }

    public Feedback(string feedbackId, string content)
    {
        FeedbackId = feedbackId;
        Content = content;
        Timestamp = DateTime.Now;
        IsFlagged = false;
    }
}