﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DidExpress.View.Windows {
    public partial class AuthWindow : Window {
        public AuthWindow() {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e) {
            if ((Owner as MainWindow).Login_Click(Login.Text, Password.Password)) {
                Close();
            }
            else {
                Login.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                Password.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                MessageBox.Show("Невірний логін або пароль!", "Помилка авторизації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
