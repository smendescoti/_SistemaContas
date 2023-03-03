using SistemaContas.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para o formulário de consulta de contas
    /// </summary>
    public class ContasConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public DateTime? DataFim { get; set; }

        /// <summary>
        /// Listagem de contas que iremos gerar após a realização da consulta
        /// </summary>
        public List<Conta>? Contas { get; set; }
    }
}
