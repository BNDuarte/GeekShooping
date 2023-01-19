namespace GeekShooping.Web.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public string? ImageURL { get; set; }
    }
}