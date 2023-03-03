using SistemaContas.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Messages.Services
{
    /// <summary>
    /// Classe que implementa o serviço de envio de emails
    /// </summary>
    public class EmailMessageService
    {
        //atributos
        private static string? _email = "csharpcoti@outlook.com";
        private static string? _senha = "@Admin12345";
        private static string? _smtp = "smtp-mail.outlook.com";
        private static int? _porta = 587;

        /// <summary>
        /// Método para executar o envio de email
        /// </summary>
        public static void EnviarMensagem(EmailMessageModel model)
        {
            #region Montando o conteúdo do email

            var mailMessage = new MailMessage(_email, model.EmailDestinatario);
            mailMessage.Subject = model.Assunto;
            mailMessage.Body = model.Mensagem;
            mailMessage.IsBodyHtml = true;

            #endregion

            #region Enviando o email

            var smtpClient = new SmtpClient(_smtp, _porta.Value);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_email, _senha);
            smtpClient.Send(mailMessage);

            #endregion
        }
    }
}
