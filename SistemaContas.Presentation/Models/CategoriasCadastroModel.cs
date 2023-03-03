using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para o formulário de cadastro de categoria
    /// </summary>
    public class CategoriasCadastroModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da categoria.")]
        public string? Nome { get; set; }
    }
}
