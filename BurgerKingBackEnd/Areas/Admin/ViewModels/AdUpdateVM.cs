using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class AdUpdateVM
    {
        [ValidateNever]
        public int Id { get; set; }

        [ValidateNever]
        public string Image { get; set; }


        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }

        [ValidateNever]
        [Display(Name = "Image", Prompt = "Please fill this input")]
        public IFormFile IImage { get; set; }
    }
}
