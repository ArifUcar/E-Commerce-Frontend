using E_Commerce_FrontEnd.Models;

public interface IOrderService
{
    Task<int> GetTotalOrderCount();
    Task<decimal> GetTotalRevenue();
    Task<List<Order>> GetRecentOrders(int count);
} 