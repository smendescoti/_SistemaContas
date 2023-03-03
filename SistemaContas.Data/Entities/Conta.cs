using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Entities
{
    public class Conta
    {
        private Guid _idConta;
        private string? _nome;
        private DateTime? _data;
        private decimal? _valor;
        private int? _tipo;
        private string? _observacoes;
        private Guid _idCategoria;
        private Guid _idUsuario;
        private Categoria? _categoria;

        public Guid IdConta { get => _idConta; set => _idConta = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public DateTime? Data { get => _data; set => _data = value; }
        public decimal? Valor { get => _valor; set => _valor = value; }
        public int? Tipo { get => _tipo; set => _tipo = value; }
        public string? Observacoes { get => _observacoes; set => _observacoes = value; }
        public Guid IdCategoria { get => _idCategoria; set => _idCategoria = value; }
        public Guid IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public Categoria? Categoria { get => _categoria; set => _categoria = value; }
    }
}
