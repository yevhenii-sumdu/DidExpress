using DidExpress.View.Windows;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DidExpress {
    public partial class MainWindow : Window {

        public User CurrentUser;

        public MainWindow() {
            InitializeComponent();

            CurrentUser = new User();
        }

        private void AuthMenuItem_Click(object sender, RoutedEventArgs e) {
            var authWindow = new AuthWindow();
            authWindow.Owner = this;
            authWindow.ShowDialog();
        }

        public bool Login_Click(string login, string password) {
            var res = CurrentUser.Login(login, password);

            if (res) {
                AuthMenuItem.Visibility = Visibility.Collapsed;
                EditMenuItem.Visibility = Visibility.Visible;
                UserInfoMenuItem.Visibility = Visibility.Visible;

                return true;
            }

            return false;
        }

        private void UserInfoMenuItem_Click(object sender, RoutedEventArgs e) {

        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e) {

        }
    }
}
