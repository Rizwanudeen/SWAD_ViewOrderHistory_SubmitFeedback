using SWAD_ViewOrderHistory_SubmitFeedback.Models;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

public class FoodStallRepository : IRepository<FoodStall>
{
    private readonly List<FoodStall> _foodStalls;

    public FoodStallRepository()
    {
        _foodStalls = new List<FoodStall>
        {
            new FoodStall("S1", "Western Food Stall", "8am-8pm", 15),
            new FoodStall("S2", "Asian Delights", "9am-9pm", 10)
        };
    }

    public List<FoodStall> GetAll() => _foodStalls;

    public FoodStall? GetById(string id) => _foodStalls.FirstOrDefault(s => s.StallId == id);

    public void Add(FoodStall entity) => _foodStalls.Add(entity);

    public void Update(FoodStall entity)
    {
        var index = _foodStalls.FindIndex(s => s.StallId == entity.StallId);
        if (index != -1)
        {
            _foodStalls[index] = entity;
        }
    }
}