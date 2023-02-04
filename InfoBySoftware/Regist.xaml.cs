using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace InfoBySoftware
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Window
    {
        public Regist()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new AuthWindow().Show();
        }

        private void RegUp_Click(object sender, RoutedEventArgs e)
        {
            string login = loginField.Text.Trim();
            string password = passwordField.Password.Trim();

            using (UserContext db = new UserContext())
            {

                User user = new User { Login = login, Password = password };

                db.Users.Add(user);
                db.SaveChanges();
                this.Close();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void changeLine_TextInput(object sender, TextCompositionEventArgs e)
        {

            if (Regex.IsMatch(e.Text, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"))
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText((TextBox)sender, "Succes");
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText((TextBox)sender, "NotSucces");
            }
        }

        private void changeLinePassword_TextInput(object sender, TextCompositionEventArgs e)
        {
            
            /*if (textBox.Password.Length >= 12)
            {
                e.Handled = true;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(textBox, "Password");
            }*/

            if (Regex.IsMatch(e.Text, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,12}$"))
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText((PasswordBox)sender, "Succes");
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText((PasswordBox)sender, "NotSucces");
            }
        }
    }
}
