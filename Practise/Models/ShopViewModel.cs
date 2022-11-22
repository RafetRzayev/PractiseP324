using Practise.DAL.Entities;

namespace Practise.Models
{
    public class ShopViewModel
    {
        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
