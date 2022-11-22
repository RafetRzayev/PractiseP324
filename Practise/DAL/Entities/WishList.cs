namespace Practise.DAL.Entities
{
    public class WishList : Entity
    {
        public string UserId { get; set; }
        public ICollection<WishListProduct> WishListProducts { get; set; }
    }
}
