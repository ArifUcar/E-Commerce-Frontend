using E_Commerce_FrontEnd.Models;

public interface IOrderService
{
    Task<List<Order>> GetMyOrders();
    Task<int> GetTotalOrderCount();
    Task<decimal> GetTotalRevenue();
    Task<List<Order>> GetRecentOrders(int count);
} 