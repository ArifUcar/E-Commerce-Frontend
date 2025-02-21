namespace E_Commerce_FrontEnd.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Base64Image { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;

        public string ImageUrl 
        { 
            get 
            {
                if (!string.IsNullOrEmpty(Base64Image))
                {
                    return $"data:image/jpeg;base64,{Base64Image}";
                }
                
                if (!string.IsNullOrEmpty(ImagePath))
                {
                    return ImagePath;
                }
                
                return "/images/no-image.jpg";
            }
            set { }  // Boş set metodu
        }

        public CartItem()
        {
            Base64Image = null;
            ImagePath = null;
        }
    }
} 