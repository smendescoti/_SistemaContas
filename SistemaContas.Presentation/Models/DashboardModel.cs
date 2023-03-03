using SistemaContas.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página dashboard
    /// </summary>
    public class DashboardModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de fim.")]
        public DateTime? DataFim { get; set; }

        public List<Conta>? ContasAReceber { get; set; }
        public List<Conta>? ContasAPagar { get; set; }

        public decimal? TotalAReceber { get; set; }
        public decimal? TotalAPagar { get; set; }
    }
}
