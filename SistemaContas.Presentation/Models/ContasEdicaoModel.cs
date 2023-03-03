using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para o formulário de edição de contas
    /// </summary>
    public class ContasEdicaoModel
    {
        public Guid IdConta { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta.")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo da conta.")]
        public int? Tipo { get; set; }

        [Required(ErrorMessage = "Por favor, informe as observações da conta.")]
        public string? Observacoes { get; set; }

        #region Campo de seleção de categoria

        [Required(ErrorMessage = "Por favor, selecione a categoria desejada.")]
        public Guid? IdCategoria { get; set; }

        public List<SelectListItem>? ItensCategoria { get; set; }

        #endregion
    }
}
