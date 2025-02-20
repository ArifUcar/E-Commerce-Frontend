namespace E_Commerce_FrontEnd.Models
{
    public class ProductCreateModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryId { get; set; }
        public string Base64Image { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }
        public string Specifications { get; set; }
        public string AdditionalInformation { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
        public string Dimensions { get; set; }
        public int StockCode { get; set; }
        public string Barcode { get; set; }

        public DateTime CreatedDate { get; set; }
    }
} 