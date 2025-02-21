using System;
using System.Collections.Generic;

namespace E_Commerce_FrontEnd.Models
{
    public class CreateOrderRequest
    {
        public Guid OrderStatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
} 