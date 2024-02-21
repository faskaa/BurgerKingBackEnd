using System.ComponentModel.DataAnnotations;

namespace BurgerKingBackEnd.ViewModels
{
    public class LoginVM
    {

        [Required]
        [Display(Prompt = "Username" , Name = "Username")]
        public string Username { get; set; }
        

        
        [Required]
        [Display(Prompt = "Password", Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
