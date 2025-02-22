using E_Commerce_FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce_FrontEnd.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetMyOrders();
        Task<int> GetTotalOrderCount();
        Task<decimal> GetTotalRevenue();

        Task<List<Order>> GetRecentOrders(int count);
        Task<bool> CreateOrder(CreateOrderRequest request);
        Task<bool> UpdateOrderStatus(Guid orderId, Guid statusId);
    }
} 