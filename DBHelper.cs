using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ScheduleManager
{
    class DBHelper
    {
        public static void checkTables(DBConnection dBConnection)
        {
            var dbCon = dBConnection;

            Dictionary<string, Dictionary<string, string>> tables = new Dictionary<string, Dictionary<string,string>>();

            Dictionary<string, string> duration = new Dictionary<string, string>();
            duration.Add("time", "int");
            duration.Add("type", "text");
            duration.Add("created_at", "timestamp");


            Dictionary<string, string> tasks = new Dictionary<string, string>();
            duration.Add("task", "text");
            duration.Add("created", "timestamp");
            duration.Add("in_between", "tinyint");
            duration.Add("active", "tinyint");
            duration.Add("current", "tinyint");

            tables.Add("duration", duration);
            tables.Add("tasks", tasks);


            if (dbCon.IsConnect())
            {
                var keys = tables.Keys;

                foreach (var key in keys)
                {
                    MySqlDataReader reader = null;
                    try
                    {
                        // check if table exists
                        string query = "SELECT * FROM information_schema.tables WHERE table_schema = '" + dbCon.DatabaseName + "' AND table_name = '" + key + "' LIMIT 1 ";
                        MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);

                        reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            //close reader
                            reader.Close();
                            reader = null;
                            
                            //create new table
                            CreateTable(dBConnection, key, tables[key]);
                        }
                        

                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Error exist " + e.Message);
                    }
                    finally
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                    
                }

                
            }
        }

        public static void CreateTable(DBConnection dBConnection, string tableName, Dictionary<string, string> data)
        {
            var dbCon = dBConnection;


            if (dbCon.IsConnect())
            {
                //string.Format("Server=localhost; database={0}; UID=UserName; password=your password", "");
                string query = "CREATE TABLE " + tableName + " ( id INT(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,";

                var keys = data.Keys.ToArray();

                for (int i = 1; i <= keys.Count(); i++)
                {
                    string key = keys[i - 1];
                    string colunmType = data[key].ToUpper();
                    if (colunmType == "INT")
                    {
                        colunmType += "(11)";
                    }

                        if (i < keys.Count())
                    {
                        query += key + " " + colunmType + ",";
                    }
                    else
                    {
                        query += key + " " + colunmType + ")";
                    }
                }

                

                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
                //dbCon.Close();

            }
        }

        public static void Insert(DBConnection dBConnection, string tableName, Dictionary<string,string> data)
        {
            var dbCon = dBConnection;
            

            if (dbCon.IsConnect())
            {
                //string.Format("Server=localhost; database={0}; UID=UserName; password=your password", "");
                string query = "INSERT INTO " + tableName + " (";

                var keys = data.Keys.ToArray();

                for (int i = 1; i <= keys.Count(); i ++) {
                    string key = keys[i - 1];

                    if (i < keys.Count())
                    {
                        query += key + ",";
                    }
                    else
                    {
                        query += key + ")";
                    }
                }

                query += " VALUES (";
                for (int i = 1; i <= keys.Count(); i++)
                {
                    string key = keys[i - 1];

                    if (i < keys.Count())
                    {
                        query += "'" + data[key] + "',";
                    }
                    else
                    {
                        query += "'" + data[key] + "')";
                    }
                }

                
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
                //dbCon.Close();

            }
        }

        public static void Update(DBConnection dBConnection, string tableName, Dictionary<string, string> data, string whereClause)
        {
            //Update table set a=1,b=2,c=3 where x=1;
            var dbCon = dBConnection;


            if (dbCon.IsConnect())
            {
                //string.Format("Server=localhost; database={0}; UID=UserName; password=your password", "");
                string query = "UPDATE " + tableName + " set ";

                var keys = data.Keys.ToArray();

                for (int i = 1; i <= keys.Count(); i++)
                {
                    string key = keys[i - 1];

                    if (i < keys.Count())
                    {
                        query += key + "='" + data[key] + "',";
                    }
                    else
                    {
                        query += key + "='" + data[key] + "'";
                    }
                }

                query += " WHERE " + whereClause;

                //System.Windows.Forms.MessageBox.Show(query);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
        }

        public static Dictionary<int, Dictionary<string, string>> Get(DBConnection dBConnection, string tableName, string whereClause="", int limit=0, string orderBy="")
        {
            var dbCon = dBConnection;
            //var data = new Dictionary<string, string>();
            var data = new Dictionary<int, Dictionary<string, string>>();

            if (dbCon.IsConnect())
            {
                MySqlDataReader reader = null;
                try
                {
                    string query = "" +
                        "SELECT * FROM " + tableName;

                    if (whereClause != "")
                    {
                        query += " WHERE " + whereClause;
                    }

                    if (orderBy != "")
                    {
                        query += " ORDER BY " + orderBy;
                    }

                    if (limit > 0)
                    {
                        query += " LIMIT " + limit;
                    }

                    int rowCount = 0;

                    if (limit == 0)
                    {
                        rowCount = Count(dbCon, tableName, whereClause);
                    }
                    else
                    {
                        rowCount = limit;
                    }

                    MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        for (int j = 0; j < rowCount; j++)
                        {
                            int fieldCount = reader.FieldCount;
                            data.Add(j, new Dictionary<string, string>());

                            for (int i = 0; i < fieldCount; i++)
                            {
                                data[j].Add(reader.GetName(i), reader.GetString(i));
                            }
                            if (rowCount > 1) reader.Read();
                        }
                    }

                    //while (reader.Read())
                    //{
                    //    string someStringFromColumnOne = reader.GetString(1);
                    //    System.Windows.Forms.MessageBox.Show("Query - " + query + " - Value - " + someStringFromColumnOne);
                    //}
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }

            return data;
        }

        public static int Count(DBConnection dBConnection, string tableName, string whereClause="")
        {
            var dbCon = dBConnection;
            int count = -1;

            if (dbCon.IsConnect())
            {
                MySqlDataReader reader = null;

                try
                {
                    string query = "" +
                        "SELECT COUNT(*) FROM " + tableName;
                    if (whereClause != "")
                    {
                        query += " WHERE " + whereClause;
                    }

                    
                    MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch { }
            }

            return count;
        }
    }
}
