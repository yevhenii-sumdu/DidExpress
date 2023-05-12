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
using System.Windows.Shapes;

namespace DidExpress.View.Windows {
    public partial class NewToyWindow : Window {
        public NewToyWindow() {
            InitializeComponent();
        }

        private void ToyAdd_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(ToyName.Text) ||
                string.IsNullOrEmpty(ToyColor.Text) ||
                string.IsNullOrEmpty(ToyAge.Text)) {

                MessageBox.Show("Заповніть усі поля", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!int.TryParse(ToyAge.Text, out _)) {
                MessageBox.Show("Невірно введені дані", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                int id = EditDB.AddToy(new Toy(0, ToyName.Text, ToyColor.Text, Convert.ToInt32(ToyAge.Text), Convert.ToInt32(ToyBag.Text)));
                string name = ToyName.Text;

                var win = (Owner as EditWindow);

                if (win.OpenedBag == null) {
                    win.Return_Click(null, null);
                }
                else if (win.OpenedBag == Convert.ToInt32(ToyBag.Text)) {
                    win.AddToyToList(name, id);
                }

                Close();
            }
        }
    }
}
