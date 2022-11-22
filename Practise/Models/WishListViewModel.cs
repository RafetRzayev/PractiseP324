namespace Practise.Models
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public decimal DiscountedPrice => Price * (100 - Discount) / 100;

    }
}
