namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class StaffResponse
{
    public string ResponseId { get; set; }
    public string ResponseContent { get; set; }
    public DateTime ResTimestamp { get; set; }

    public StaffResponse(string responseId, string responseContent, DateTime resTimestamp)
    {
        ResponseId = responseId;
        ResponseContent = responseContent;
        ResTimestamp = resTimestamp;
    }
}