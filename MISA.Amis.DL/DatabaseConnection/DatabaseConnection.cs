using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL
{
    public class DatabaseConnection : IDatabaseConnection
    {
        /// <summary>
        /// Đối tượng kết nối tới database
        /// </summary>
        /// <returns>Đối tượng MySqlConnection</returns>
        /// Created by: VĂn Anh (6/2/2023)
        public MySqlConnection ConnectDatabase() 
        {
            var mySqlConnectionString = DatabaseContext.ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(mySqlConnectionString);
            return mySqlConnection;
        }
    }
}
