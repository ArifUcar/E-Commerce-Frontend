namespace E_Commerce_FrontEnd.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public bool IsDiscounted
        {
            get
            {
                return DiscountRate > 0 &&
                       DiscountStartDate <= DateTime.Now &&
                       DiscountEndDate >= DateTime.Now;
            }
        }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public string? Base64Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductDetail? ProductDetail { get; set; }

        // İndirim hesaplamaları için yardımcı özellikler
        public decimal CurrentPrice => IsDiscounted ? DiscountedPrice.Value : Price;

        public string ImageUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(Base64Image))
                    return $"data:image/jpeg;base64,{Base64Image}";
                
                return string.IsNullOrEmpty(ImagePath) ? "/images/no-image.jpg" : ImagePath;
            }
        }
    }

    public class ProductDetail
    {
        public Guid Id { get; set; }
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