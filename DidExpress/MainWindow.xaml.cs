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
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

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
                if (CurrentUser.EditAccess) {
                    EditMenuItem.Visibility = Visibility.Visible;
                }

                AuthMenuItem.Visibility = Visibility.Collapsed;             
                UserInfoMenuItem.Visibility = Visibility.Visible;

                return true;
            }

            return false;
        }

        private void UserInfoMenuItem_Click(object sender, RoutedEventArgs e) {
            string name = CurrentUser.UserName;
            string login = CurrentUser.UserLogin;
            string editAccess = CurrentUser.EditAccess ? "Є" : "Немає";

            MessageBox.Show($"Користувач: {name}\nЛогін: {login}\nДоступ до редагування: {editAccess}", "Інформація про користувача", MessageBoxButton.OK, MessageBoxImage.Information); ;
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
                var age = Convert.ToInt32(AgeTextBox.Text);
                var toys = SelectToy.SelectByAge(age);

                if (toys.Count == 0) {
                    MessageBox.Show("Іграшок для заданого віку не знайдено", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                SearchResultsTextBlock.Visibility = Visibility.Visible;

                Save.Visibility = Visibility.Visible;

                SearchRow.Height = new GridLength(120);

                SearchResultsRow.Height = new GridLength(1, GridUnitType.Star);

                SearchResults.Children.Clear();

                var groupedToys = toys.Where(t => t.Age == age)
                                      .GroupBy(t => t.Bag);

                foreach (var group in groupedToys) {
                    var textBlock = new TextBlock() { Style = (Style)FindResource("SearchResultTextBlock") };
                    textBlock.Text = $"Мішок {group.Key} ({group.Count()})";

                    SearchResults.Children.Add(textBlock);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e) {

        }
    }
}
