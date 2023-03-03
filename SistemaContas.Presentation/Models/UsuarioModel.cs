namespace SistemaContas.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para armazenar as informações do usuário autenticado
    /// </summary>
    public class UsuarioModel
    {
        public Guid IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }
    }
}
