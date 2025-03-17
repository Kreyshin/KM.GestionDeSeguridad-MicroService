using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Infraestructura.Persistencia
{
    public class DbConexion
    {
        private readonly string _connectionString;

        public DbConexion(IDbConfiguracion dbConfiguracion)
        {
            _connectionString = dbConfiguracion.ConnectionString;
        }

        public IDbConnection CrearConexion => new SqlConnection(_connectionString);
    }
}
