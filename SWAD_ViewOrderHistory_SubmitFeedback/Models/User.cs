namespace SWAD_ViewOrderHistory_SubmitFeedback.Models;

public abstract class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    protected User(string id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }
}