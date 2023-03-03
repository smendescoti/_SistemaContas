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
    /// Repositório de dados para usuário
    /// </summary>
    public class UsuarioRepository
    {
        /// <summary>
        /// Método para inserir um usuário no banco de dados
        /// </summary>
        public void Inserir(Usuario usuario)
        {
            var query = @"
                INSERT INTO USUARIO(
                    IDUSUARIO,
                    NOME,
                    EMAIL,
                    SENHA,
                    DATAHORACRIACAO)
                VALUES(
                    @IdUsuario,
                    @Nome,
                    @Email,
                    CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                    @DataHoraCriacao)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, usuario);
            }
        }

        /// <summary>
        /// Método para alterar a senha do usuário
        /// </summary>
        public void Atualizar(Guid? idUsuario, string? novaSenha)
        {
            var query = @"
                UPDATE USUARIO 
                SET 
                    SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @novaSenha), 2)
                WHERE
                    IDUSUARIO = @idUsuario
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, new { idUsuario, novaSenha });
            }
        }

        /// <summary>
        /// Método para consultar 1 usuário através do email
        /// </summary>
        public Usuario? ObterPorEmail(string email)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { email }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para consultar 1 usuário através do email e da senha
        /// </summary>
        public Usuario? ObterPorEmailESenha(string email, string senha)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email 
                AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @senha), 2)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection.Query<Usuario>(query, new { email, senha }).FirstOrDefault();
            }
        }

    }
}
