public class UpdateOrderRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid OrderStatusId { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string ZipCode { get; set; }
    public List<OrderDetailRequest> OrderDetails { get; set; }
}

public class OrderDetailRequest
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }
} 