﻿using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DidExpress.View.Windows {
    public partial class EditWindow : Window {
        public EditWindow() {
            InitializeComponent();
        }

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

        public int? OpenedBag = null;

        private void EditBag_Click(object sender, RoutedEventArgs e) {
            int bag = Convert.ToInt32((sender as Button).Name.Replace("EditBag", ""));
            ListHeader.Text = $"Список іграшок в мішку {bag}";

            OpenedBag = bag;

            List.Children.Clear();
            
            foreach (var toy in DataAccess.LoadToysFromBag(bag)) {
                AddToyToList(toy.Name, toy.Id);
            }

            Add.Visibility = Visibility.Visible;
            Return.Visibility = Visibility.Visible;
        }

        private void DeleteToy_Click(object sender, RoutedEventArgs e) {
            FindParent<Grid>(sender as Button).Visibility = Visibility.Collapsed;

            int id = Convert.ToInt32((sender as Button).Name.Replace("DeleteToy", ""));
            EditDB.DeleteToy(id);
        }

        public Toy EditedToy;
        public Grid EditedToyGrid;

        private void EditToy_Click(object sender, RoutedEventArgs e) {
            EditedToyGrid = FindParent<Grid>(sender as Button);
            var editToyWindow = new EditToyWindow();
            editToyWindow.Owner = this;

            EditedToy = DataAccess.GetToyById(Convert.ToInt32((sender as Button).Name.Replace("EditToy", "")));
            editToyWindow.ToyName.Text = EditedToy.Name;
            editToyWindow.ToyColor.Text = EditedToy.Color;
            editToyWindow.ToyAge.Text = EditedToy.Age.ToString();
            editToyWindow.ToyBag.Text = EditedToy.Bag.ToString();

            editToyWindow.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            var NtWindow = new NewToyWindow();
            NtWindow.Owner = this;
            NtWindow.ShowDialog();
        }

        public void Return_Click(object sender, RoutedEventArgs e) {
            ListHeader.Text = "Список мішків";

            List.Children.Clear();

            var bags = DataAccess.LoadBags();

            foreach (var bag in bags) {
                AddBagToList(bag);
            }

            Return.Visibility = Visibility.Hidden;

            OpenedBag = null;
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

        public void AddToyToList(string name, int id) {
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
