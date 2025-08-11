namespace SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

public interface IRepository<T>
{
    List<T> GetAll();
    T? GetById(string id);
    void Add(T entity);
    void Update(T entity);
}