using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class ProductCreateVM
    {
        [ValidateNever]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Title", Prompt = "Please fill this input")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description", Prompt = "Please fill this input")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price", Prompt = "Please fill this input")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Calory", Prompt = "Please fill this input")]
        public int Calory { get; set; }


        [Required]
        [Display(Name = "Image", Prompt = "Please fill this input")]
        public IFormFile IImage { get; set; }
    }
}
