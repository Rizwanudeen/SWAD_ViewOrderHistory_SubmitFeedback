namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public class Student : User
{
    public string StudentId { get; set; }
    public List<Order> Orders { get; set; } = new();

    public Student(string id, string name, string studentId) : base(id, name)
    {
        StudentId = studentId;
    }
}