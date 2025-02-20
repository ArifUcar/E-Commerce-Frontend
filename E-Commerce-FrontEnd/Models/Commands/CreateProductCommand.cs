using System;

namespace E_Commerce_FrontEnd.Models.Commands
{
    public class CreateProductCommand
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public string? Base64Image { get; set; }
        public ProductDetailCommand? ProductDetail { get; set; }
    }

    public class ProductDetailCommand
    {
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Material { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Warranty { get; set; }
        public string? Specifications { get; set; }
        public string? AdditionalInformation { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightUnit { get; set; }
        public string? Dimensions { get; set; }
        public int? StockCode { get; set; }
        public string? Barcode { get; set; }
    }
} 