using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL
{
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Interface kết nối tới database
        /// </summary>
        /// <returns>Đối tượng MySqlConnection</returns>
        /// Created by: VĂn Anh (6/2/2023)
        MySqlConnection ConnectDatabase();
    }
}
