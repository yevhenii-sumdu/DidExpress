using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DidExpress.View.Windows {
    public partial class EditWindow : Window {
        public EditWindow() {
            InitializeComponent();
        }

        string Mode = "Bags";

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var bags = DataAccess.LoadBags();

            foreach (var bag in bags) {
                AddBagToList(bag);
            }
        }

        private void DeleteBag_Click(object sender, RoutedEventArgs e) {
            FindParent<Grid>(sender as Button).Visibility = Visibility.Collapsed;

            int bag = Convert.ToInt32((sender as Button).Name.Replace("DeleteBag", ""));
            EditDB.DeleteBag(bag);
        }

        private void EditBag_Click(object sender, RoutedEventArgs e) {
            int bag = Convert.ToInt32((sender as Button).Name.Replace("EditBag", ""));
            ListHeader.Text = $"Список іграшок в мішку {bag}";

            List.Children.Clear();
            
            foreach (var toy in DataAccess.LoadToysFromBag(bag)) {
                AddToyToList(toy.Name, toy.Id);

                Mode = "Toys";
            }
        }

        private void DeleteToy_Click(object sender, RoutedEventArgs e) {
            FindParent<Grid>(sender as Button).Visibility = Visibility.Collapsed;

            int id = Convert.ToInt32((sender as Button).Name.Replace("DeleteToy", ""));
            EditDB.DeleteToy(id);
        }

        private void EditToy_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show($"Editing toy {(sender as Button).Name.Replace("Edit", "")}");
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            //if (Mode == "Bags") {
            //    AddBagToList(Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds()));
            //}
            //else {
            //    AddToyToList(DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
            //}
        }

        void AddBagToList(int i) {
            Grid grid = new Grid();

            var textBlock = new TextBlock() { Style = (Style)FindResource("ListTextBlock") };
            textBlock.Text = "Мішок " + i.ToString();

            var deleteButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            deleteButton.Margin = new Thickness(10, 5, 10, 5);
            deleteButton.Name = $"DeleteBag{i}";
            deleteButton.Click += DeleteBag_Click;

            var deleteIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            deleteIcon.Kind = PackIconKind.Delete;

            deleteButton.Content = deleteIcon;

            Button editButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            editButton.Margin = new Thickness(10, 5, 60, 5);
            editButton.Name = $"EditBag{i}";
            editButton.Click += EditBag_Click;

            PackIcon editIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            editIcon.Kind = PackIconKind.Edit;

            editButton.Content = editIcon;

            grid.Children.Add(textBlock);
            grid.Children.Add(deleteButton);
            grid.Children.Add(editButton);

            List.Children.Add(grid);
        }

        void AddToyToList(string name, int id) {
            Grid grid = new Grid();

            var textBlock = new TextBlock() { Style = (Style)FindResource("ListTextBlock") };
            textBlock.Text = "Іграшка " + name;

            var deleteButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            deleteButton.Margin = new Thickness(10, 5, 10, 5);
            deleteButton.Name = $"DeleteToy{id}";
            deleteButton.Click += DeleteToy_Click;

            var deleteIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            deleteIcon.Kind = PackIconKind.Delete;

            deleteButton.Content = deleteIcon;

            Button editButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            editButton.Margin = new Thickness(10, 5, 60, 5);
            editButton.Name = $"EditToy{id}";
            editButton.Click += EditToy_Click;

            PackIcon editIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            editIcon.Kind = PackIconKind.Edit;

            editButton.Content = editIcon;

            grid.Children.Add(textBlock);
            grid.Children.Add(deleteButton);
            grid.Children.Add(editButton);

            List.Children.Add(grid);
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject {
            T parent = VisualTreeHelper.GetParent(child) as T;

            if (parent != null)
                return parent;
            else
                return FindParent<T>(parent);
        }
    }
}
