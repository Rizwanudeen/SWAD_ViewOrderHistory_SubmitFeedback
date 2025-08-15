using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

public class FeedbackRepository : IRepository<Feedback>
{
    private readonly List<Feedback> _feedbacks = new();

    public List<Feedback> GetAll() => _feedbacks;

    public Feedback? GetById(string id) => _feedbacks.FirstOrDefault(f => f.FeedbackId == id);

    public void Add(Feedback entity) => _feedbacks.Add(entity);

    public void Update(Feedback entity)
    {
        var index = _feedbacks.FindIndex(f => f.FeedbackId == entity.FeedbackId);
        if (index != -1)
        {
            _feedbacks[index] = entity;
        }
    }

    public bool ExistsForFeedbackId(string feedbackId) => _feedbacks.Any(f => f.FeedbackId == feedbackId);
}