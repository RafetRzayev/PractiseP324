namespace Practise.DAL.Entities
{
    public class WishListProduct : Entity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WishListId { get; set; }
        public WishList WishList { get; set; }
    }
}
