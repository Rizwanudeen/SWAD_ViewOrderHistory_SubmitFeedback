namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class Student : User
{
    public int NoShowCount { get; set; }
    public bool IsSuspended { get; set; }
    public string Type { get; set; } // "Regular" or "Priority"
    public DateTime? SuspensionEndDate { get; set; }

    public Student(string id, string name, string password, int noShowCount, bool isSuspended, string type, DateTime? suspensionEndDate) : base(id, name, password)
    {
        NoShowCount = noShowCount;
        IsSuspended = isSuspended;
        Type = type;
        SuspensionEndDate = suspensionEndDate;
    }
}