namespace E_Commerce_FrontEnd.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
} 