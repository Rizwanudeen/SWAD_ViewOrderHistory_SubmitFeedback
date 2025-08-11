namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public abstract class User
{
    public string Id { get; set; }
    public string Name { get; set; }

    protected User(string id, string name)
    {
        Id = id;
        Name = name;
    }
}