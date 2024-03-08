using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class ProductUpdateVM
    {

        public int Id { get; set; }
        [ValidateNever]
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Calory { get; set; }
        [ValidateNever]
        public IFormFile IImage { get; set; }
    }
}
