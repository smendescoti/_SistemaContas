using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Messages.Models
{
    /// <summary>
    /// Modelo de dados para envio de emails
    /// </summary>
    public class EmailMessageModel
    {
        /// <summary>
        /// Email do destinatário da mensagem
        /// </summary>
        public string? EmailDestinatario { get; set; }

        /// <summary>
        /// Assunto da mensagem
        /// </summary>
        public string? Assunto { get; set; }

        /// <summary>
        /// Corpo da mensagem
        /// </summary>
        public string? Mensagem { get; set; }
    }
}
