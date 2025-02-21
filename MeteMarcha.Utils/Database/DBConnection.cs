using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteMarcha.Utils.Database
{
    public class DBConnection
    {
        public const string CONNECTION_STRING = "Server=localhost;Database=metemarcha;User ID=root;Password=root;";

        public static bool TestarConexao()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
