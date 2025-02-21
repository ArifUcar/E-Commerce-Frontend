using System;

namespace E_Commerce_FrontEnd.Models;

public class OrderDetail
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
}

