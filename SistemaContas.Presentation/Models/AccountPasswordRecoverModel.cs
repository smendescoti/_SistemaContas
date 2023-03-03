using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para o formulário de recuperação de senha de usuário
    /// </summary>
    public class AccountPasswordRecoverModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }
    }
}
