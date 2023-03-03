using Dapper;
using SistemaContas.Data.Configurations;
using SistemaContas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Repositories
{
    /// <summary>
    /// Repositório de dados para categoria
    /// </summary>
    public class CategoriaRepository
    {
        /// <summary>
        /// Método para inserir uma categoria no banco de dados
        /// </summary>
        public void Inserir(Categoria categoria)
        {
            var query = @"
                INSERT INTO CATEGORIA(IDCATEGORIA, NOME, IDUSUARIO)
                VALUES(@IdCategoria, @Nome, @IdUsuario)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, categoria);
            }
        }

        /// <summary>
        /// Método para atualizar uma categoria no banco de dados
        /// </summary>
        public void Atualizar(Categoria categoria)
        {
            var query = @"
                UPDATE CATEGORIA SET NOME = @Nome 
                WHERE IDCATEGORIA = @IdCategoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, categoria);
            }
        }

        /// <summary>
        /// Método para excluir uma categoria no banco de dados
        /// </summary>
        public void Excluir(Categoria categoria)
        {
            var query = @"
                DELETE FROM CATEGORIA
                WHERE IDCATEGORIA = @IdCategoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, categoria);
            }
        }

        /// <summary>
        /// Método para consultar categorias no banco de dados
        /// </summary>
        public List<Categoria> ObterTodos(Guid idUsuario)
        {
            var query = @"
                SELECT * FROM CATEGORIA
                WHERE IDUSUARIO = @idUsuario
                ORDER BY NOME
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Categoria>(query, new { idUsuario }).ToList();
            }
        }

        /// <summary>
        /// Método para consultar 1 categoria no banco de dados através do ID
        /// </summary>
        public Categoria? ObterPorId(Guid idCategoria)
        {
            var query = @"
                SELECT * FROM CATEGORIA
                WHERE IDCATEGORIA = @idCategoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Categoria>(query, new { idCategoria }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para consultar a quantidade de contas associadas a uma categoria
        /// </summary>
        public int? ObterQuantidadeContas(Guid? idCategoria)
        {
            var query = @"
                SELECT COUNT(IDCONTA) FROM CONTA
                WHERE IDCATEGORIA = @idCategoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<int>(query, new { idCategoria }).FirstOrDefault();
            }
        }
    }
}
