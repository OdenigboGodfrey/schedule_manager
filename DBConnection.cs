using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ScheduleManager
{
    class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public String Password { get; set; }
        private MySqlConnection connection = null;

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                try
                {
                    
                    if (String.IsNullOrEmpty(databaseName)) return false;
                    
                    String connstring = String.Format("Server=localhost; database='" + databaseName + "'; UID=root; password=");
                    connection = new MySqlConnection(connstring);
                    connection.Open();

                    return true;
                }
                catch (MySqlException ex)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
