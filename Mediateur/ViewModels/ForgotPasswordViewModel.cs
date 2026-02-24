using System.ComponentModel.DataAnnotations;

namespace Mediateur.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
