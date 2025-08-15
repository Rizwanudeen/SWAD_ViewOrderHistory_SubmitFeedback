using SWAD_ViewOrderHistory_SubmitFeedback.Models;
using SWAD_ViewOrderHistory_SubmitFeedback.Repositories;

namespace SWAD_ViewOrderHistory_SubmitFeedback.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly FoodStallRepository _foodStallRepository;

    public OrderService(OrderRepository orderRepository, FoodStallRepository foodStallRepository)
    {
        _orderRepository = orderRepository;
        _foodStallRepository = foodStallRepository;
    }

    public List<Order> GetOrderHistory(string studentId)
    {
        return _orderRepository.GetAll();
    }

    public Order? GetOrderById(string orderId)
    {
        return _orderRepository.GetById(orderId);
    }

    public FoodStall? GetFoodStall(string stallId)
    {
        return _foodStallRepository.GetById(stallId);
    }

    public bool IsOrderEligibleForFeedback(Order order)
    {
        return order.Status == "Collected";
    }

    public void UpdateOrder(Order order)
    {
        _orderRepository.Update(order);
    }
}