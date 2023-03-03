using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Entities
{
    /// <summary>
    ///  Modelo de entidade
    /// </summary>
    public class Categoria
    {
        private Guid _idCategoria;
        private string _nome;
        private Guid _idUsuario;

        public Guid IdCategoria { get => _idCategoria; set => _idCategoria = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public Guid IdUsuario { get => _idUsuario; set => _idUsuario = value; }
    }
}
