using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Configurations
{
    /// <summary>
    /// Classe de configurações para o SqlServer
    /// </summary>
    public class SqlServerConfiguration
    {
        /// <summary>
        /// Método para retornar a string de conexão do banco de dados
        /// </summary>
        public static string ConnectionString
            => @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDSistemaContas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
