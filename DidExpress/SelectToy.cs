using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static DidExpress.Utils;

namespace DidExpress {
    class SelectToy {
        static string _connStr = DatabaseSettings.ConnectionString;

        public static List<Toy> SelectByAge(int age) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            List<Toy> res = new List<Toy>();

            try {
                conn.Open();

                string sql = $"SELECT * FROM Toys WHERE age = {age}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res.Add(new Toy(Convert.ToInt32(rdr[0]), rdr[1].ToString(), rdr[2].ToString(), Convert.ToInt32(rdr[3]), Convert.ToInt32(rdr[4])));
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка підключення до сервера");
            }

            conn.Close();

            return res;
        }
    }
}
