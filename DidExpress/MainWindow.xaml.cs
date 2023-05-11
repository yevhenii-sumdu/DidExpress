using DidExpress.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            MessageBox.Show($"Користувач: {CurrentUser.UserName}\nЛогін: {CurrentUser.UserLogin}", "Інформація про користувача", MessageBoxButton.OK, MessageBoxImage.Information); ;
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e) {
            var editWindow = new EditWindow();
            editWindow.Owner = this;
            editWindow.ShowDialog();
        }

        private void Search_Click(object sender, RoutedEventArgs e) {
            var regex = new Regex("[0-9]+");
        
            if (!regex.IsMatch(AgeTextBox.Text)) {
                MessageBox.Show("Невірно введений вік!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                SearchResultsTextBlock.Visibility = Visibility.Visible;

                Save.Visibility = Visibility.Visible;

                SearchRow.Height = new GridLength(120);

                SearchResultsRow.Height = new GridLength(1, GridUnitType.Star);

                SearchResults.Children.Clear();

                for (int i = 1; i <= Convert.ToInt32(AgeTextBox.Text); i++) {
                    var textBlock = new TextBlock() { Style = (Style)FindResource("SearchResultTextBlock") };
                    textBlock.Text = "Text " + i.ToString();

                    SearchResults.Children.Add(textBlock);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e) {

        }
    }
}
