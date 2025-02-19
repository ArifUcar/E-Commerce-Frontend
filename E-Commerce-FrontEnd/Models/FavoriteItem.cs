namespace E_Commerce_FrontEnd.Models
{
    public class FavoriteItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public bool IsDiscounted => DiscountedPrice.HasValue && DiscountedPrice < Price;
        public int? DiscountRate => IsDiscounted ? 
            (int)(100 - (DiscountedPrice.Value / Price * 100)) : null;
    }
} 