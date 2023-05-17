using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace DidExpress {
    public class User {
        private string _connStr = "server=localhost;user=user;database=didexpress;port=3306;password=pass";
        public string UserName { get; private set;  }
        public string UserLogin { get; private set; }
        public bool EditAccess { get; private set; }

        public bool Login(string login, string password) {
            bool res = false;

            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"SELECT * FROM Users WHERE login = '{login}' AND password = '{sha256(password)}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    UserLogin = rdr[0].ToString();
                    UserName = rdr[2].ToString();
                    EditAccess = Convert.ToBoolean(rdr[3]);
                    res = true;
                }

                rdr.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Помилка підключення до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conn.Close();

            return res;
        }

        private string sha256(string str) {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte theByte in crypto) {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
