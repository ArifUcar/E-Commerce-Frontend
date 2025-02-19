namespace E_Commerce_FrontEnd.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
        public string Base64Image { get; set; }

        // İndirim hesaplamaları için özellikler
        public bool IsDiscounted => DiscountedPrice.HasValue && DiscountedPrice < Price;
        public int? DiscountRate => IsDiscounted ? 
            (int)(100 - (DiscountedPrice.Value / Price * 100)) : null;
    }
} 