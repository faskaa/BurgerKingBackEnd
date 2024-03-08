using BurgerKingBackEnd.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class PRCreateVM
    {

        public int RestaurantId { get; set; }
        [ValidateNever]
        public Restaurant Restaurant { get; set; }


        public int ProductId { get; set; }
        [ValidateNever]

        public Product Product { get; set; }

        public int Count { get; set; }

        [ValidateNever]
        public List<Restaurant> restaurant { get; set; }

        [ValidateNever]
        public List<Product> product { get; set; }
    }
}
