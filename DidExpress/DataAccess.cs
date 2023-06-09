﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using static DidExpress.Utils;

namespace DidExpress {
    public class DataAccess {
        static string _connStr = DatabaseSettings.ConnectionString;

        public static List<int> LoadBags() {          
            MySqlConnection conn = new MySqlConnection(_connStr);

            List<int> res = new List<int>();

            try {
                conn.Open();

                string sql = "SELECT DISTINCT(bag) FROM Toys";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res.Add(Convert.ToInt32(rdr[0]));
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка завантаження даних");
            }

            conn.Close();

            return res;
        }

        public static List<Toy> LoadToysFromBag(int bag) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            List<Toy> res = new List<Toy>();

            try {
                conn.Open();

                string sql = $"SELECT * FROM Toys WHERE bag = {bag}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res.Add(new Toy(Convert.ToInt32(rdr[0]), rdr[1].ToString(), rdr[2].ToString(), Convert.ToInt32(rdr[3]), Convert.ToInt32(rdr[4])));
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка завантаження даних");
            }

            conn.Close();

            return res;
        }

        public static Toy GetToyById(int id) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            Toy res = null;

            try {
                conn.Open();

                string sql = $"SELECT * FROM Toys WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res = new Toy(Convert.ToInt32(rdr[0]), rdr[1].ToString(), rdr[2].ToString(), Convert.ToInt32(rdr[3]), Convert.ToInt32(rdr[4]));
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка завантаження даних");
            }

            conn.Close();

            return res;
        }
    }
}
