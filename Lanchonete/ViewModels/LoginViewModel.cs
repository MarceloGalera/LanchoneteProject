//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Lanchonete.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]   // exibe o campo de senha em um formato não legível
        [Display(Name = "Senha")]
        public string Password { get; set; }

        // esse returnUrl é para levar o usuário de volta a página que ele estava tentando acessar antes do login
        public string ReturnUrl { get; set; }
    }
}
