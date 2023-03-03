using SistemaContas.Presentation.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de alteração de senha do usuário
    /// </summary>
    public class AlterarSenhaModel
    {
        [StrongPassword(ErrorMessage = "Por favor, informe uma senha forte de 8 a 20 caracteres com pelo menos 1 letra minúscula, 1 letra maiúscula, 1 número e 1 caractere especial")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string? NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a senha do usuário.")]
        public string? NovaSenhaConfirmacao { get; set; }
    }
}
