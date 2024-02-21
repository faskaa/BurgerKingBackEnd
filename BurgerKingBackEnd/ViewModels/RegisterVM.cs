using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.ViewModels
{
    public class RegisterVM
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Prompt ="Email")]
        public string Email { get; set; }


        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 5)]
        [Display(Prompt ="Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt ="Password")]
        public string Password { get; set; }



        [Required]
        [DataType(DataType.Password) , Compare(nameof(Password))]
        [Display(Name="Confirm Password",Prompt ="Confirm Password")]
        public string ConfirmPassword { get; set; }


    }
}
