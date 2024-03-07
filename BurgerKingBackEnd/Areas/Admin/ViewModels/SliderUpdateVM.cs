using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.Areas.Admin.ViewModels
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ValidateNever]
        public string Image { get; set; }

        [ValidateNever]
        public string OrderImage { get; set; }


        [ValidateNever]
        public string DeliveryImage { get; set; }

        [ValidateNever]
        [Display(Name = "Image", Prompt = "Please fill this input")]
        public IFormFile IImage { get; set; }


        [ValidateNever]
        [Display(Name = "Order Image", Prompt = "Please fill this input")]
        public IFormFile IOrderImage { get; set; }

        [ValidateNever]
        [Display(Name = "Delivery Image", Prompt = "Please fill this input")]
        public IFormFile IDeliveryImage { get; set; }
    }
}
