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
    /// Repositório de dados para conta
    /// </summary>
    public class ContaRepository
    {
        public void Inserir(Conta conta)
        {
            var query = @"
                INSERT INTO CONTA(IDCONTA, NOME, DATA, VALOR, TIPO, OBSERVACOES, IDUSUARIO, IDCATEGORIA)
                VALUES(@IdConta, @Nome, @Data, @Valor, @Tipo, @Observacoes, @IdUsuario, @IdCategoria)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, conta);
            }
        }

        public void Atualizar(Conta conta)
        {
            var query = @"
                UPDATE CONTA 
                SET     
                    NOME = @Nome, 
                    DATA = @Data, 
                    VALOR = @Valor, 
                    TIPO = @Tipo,
                    OBSERVACOES = @Observacoes,
                    IDCATEGORIA = @IdCategoria 
                WHERE 
                    IDCONTA = @IdConta
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, conta);
            }
        }

        public void Excluir(Conta conta)
        {
            var query = @"
                DELETE FROM CONTA
                WHERE IDCONTA = @IdConta
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                connection.Execute(query, conta);
            }
        }

        public List<Conta> ObterTodos(DateTime dataInicio, DateTime dataFim, Guid idUsuario)
        {
            var query = @"
                SELECT * FROM CONTA
                INNER JOIN CATEGORIA
                ON CATEGORIA.IDCATEGORIA = CONTA.IDCATEGORIA 
                WHERE CONTA.DATA BETWEEN @dataInicio AND @dataFim AND CONTA.IDUSUARIO = @idUsuario
                ORDER BY CONTA.DATA DESC
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection
                    .Query(query, 
                        (Conta conta, Categoria categoria) =>
                        {
                            conta.Categoria = categoria;
                            return conta;
                        },
                        new { dataInicio, dataFim, idUsuario },
                        splitOn: "IdCategoria")
                    .ToList();
            }
        }

        public Conta? ObterPorId(Guid idConta)
        {
            var query = @"
                SELECT * FROM CONTA
                INNER JOIN CATEGORIA
                ON CATEGORIA.IDCATEGORIA = CONTA.IDCATEGORIA
                WHERE CONTA.IDCONTA = @idConta
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.ConnectionString))
            {
                return connection
                    .Query(query, 
                        (Conta conta, Categoria categoria) =>
                        {
                            conta.Categoria = categoria;
                            return conta;
                        },
                        new { idConta }, 
                        splitOn: "IdCategoria")
                    .FirstOrDefault();
            }
        }
    }
}
