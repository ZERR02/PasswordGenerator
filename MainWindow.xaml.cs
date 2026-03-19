using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        List<string> passwords = new List<string>();
        DatabaseHelper db = new DatabaseHelper();

        public MainWindow()
        {
            InitializeComponent();
            LoadPasswords();
        }

        void LoadPasswords()
        {
            passwords = db.LoadPasswords();
            passwors.Items.Clear();
            foreach (string p in passwords)
            {
                passwors.Items.Add(p);
            }
        }

        string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(chars.Length);
                result.Append(chars[index]);
            }

            return result.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = GeneratePassword();
            textb.Text = "Your password: " + newPassword;

            db.SavePassword(newPassword);
            LoadPasswords();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwors.SelectedItem == null)
            {
                MessageBox.Show("choose a password!", "error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedPassword = passwors.SelectedItem.ToString();

            MessageBoxResult result = MessageBox.Show(
                $"delete password: {selectedPassword}?",
                "confirmation of deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                db.DeletePassword(selectedPassword);


                LoadPasswords();

    
                passwors.SelectedItem = null;

                MessageBox.Show("password deleted!", "done",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}