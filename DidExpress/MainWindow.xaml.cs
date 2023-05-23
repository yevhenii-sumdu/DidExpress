using DidExpress.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static DidExpress.Utils;

namespace DidExpress {
    public partial class MainWindow : Window {

        public User CurrentUser;
        Dictionary<int, int> SearchResultsDict = null;
        int SearchResultsAge;

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
                ExitMenuItem.Visibility = Visibility.Visible;

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

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) {
            CurrentUser = new User();

            AuthMenuItem.Visibility = Visibility.Visible;
            EditMenuItem.Visibility = Visibility.Collapsed;
            UserInfoMenuItem.Visibility = Visibility.Collapsed;
            ExitMenuItem.Visibility = Visibility.Collapsed;
        }

        private void Search_Click(object sender, RoutedEventArgs e) {
            if (!int.TryParse(AgeTextBox.Text, out _)) {
                ShowError("Невірно введений вік!");
            }
            else {
                var age = Convert.ToInt32(AgeTextBox.Text);
                var toys = SelectToy.SelectByAge(age);

                if (toys.Count == 0) {
                    ShowError("Іграшок для заданого віку не знайдено");
                    return;
                }

                SearchResultsTextBlock.Visibility = Visibility.Visible;

                Save.Visibility = Visibility.Visible;

                SearchRow.Height = new GridLength(120);

                SearchResultsRow.Height = new GridLength(1, GridUnitType.Star);

                SearchResults.Children.Clear();

                var groupedToys = toys.Where(t => t.Age == age)
                                      .GroupBy(t => t.Bag);

                SearchResultsAge = age;
                SearchResultsDict = new Dictionary<int, int>();

                foreach (var group in groupedToys) {
                    var textBlock = new TextBlock() { Style = (Style)FindResource("SearchResultTextBlock") };
                    textBlock.Text = $"Мішок {group.Key} ({group.Count()})";

                    SearchResultsDict[group.Key] = group.Count();

                    SearchResults.Children.Add(textBlock);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            if (SearchResultsDict != null) {
                string res = WordWriter.WriteToFile(SearchResultsAge, SearchResultsDict);

                if (res != null) {
                    MessageBox.Show($"Результати пошуку збережено до файлу:\n {res}", "Файл збережено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else {
                    ShowError("Під час запису результатів пошуку до файлу виникла помилка");
                }
            }
        }

        private void AgeTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Search_Click(null, null);
            }
        }
    }
}
