using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DidExpress {
    class EditDB {
        static string _connStr = "server=localhost;user=user;database=didexpress;port=3306;password=pass";

        public static int AddToy(Toy newToy) {
            int id = 0;

            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"INSERT INTO Toys (name, color, age, bag) VALUES ('{newToy.Name}','{newToy.Color}', {newToy.Age}, {newToy.Bag})";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "SELECT LAST_INSERT_ID();";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    id = Convert.ToInt32(rdr[0]);
                }

                rdr.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Помилка підключення до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conn.Close();

            return id;
        }

        public static void DeleteToy(int id) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"DELETE FROM Toys WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                MessageBox.Show("Помилка підключення до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conn.Close();
        }

        public static void EditToy(int id, string color, int age, int bag) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"UPDATE Toys SET color = '{color}', age = {age}, bag = {bag} WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                MessageBox.Show("Помилка підключення до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conn.Close();
        }

        public static void DeleteBag(int bag) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"DELETE FROM Toys WHERE bag = {bag}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                MessageBox.Show("Помилка підключення до сервера", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conn.Close();
        }
    }
}
