using System;
using System.Windows;
using static DidExpress.Utils;

namespace DidExpress.View.Windows {
    public partial class EditToyWindow : Window {
        public EditToyWindow() {
            InitializeComponent();
        }

        int CurrentToyBag;

        private void ToySave_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(ToyName.Text) ||
                string.IsNullOrEmpty(ToyColor.Text) ||
                string.IsNullOrEmpty(ToyAge.Text) ||
                string.IsNullOrEmpty(ToyBag.Text)) {

                ShowError("Заповніть усі поля");
            }
            else if (!int.TryParse(ToyAge.Text, out _) || !int.TryParse(ToyBag.Text, out _)) {
                ShowError("Невірно введені дані");
            }
            else {
                int id = (Owner as EditWindow).EditedToy.Id;
                EditDB.EditToy(id, ToyColor.Text, Convert.ToInt32(ToyAge.Text), Convert.ToInt32(ToyBag.Text));

                if (CurrentToyBag != Convert.ToInt32(ToyBag.Text)) {
                    (Owner as EditWindow).EditedToyGrid.Visibility = Visibility.Collapsed;
                }
                
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            CurrentToyBag = Convert.ToInt32(ToyBag.Text);
        }
    }
}
