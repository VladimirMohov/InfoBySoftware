using System;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace InfoBySoftware
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public static string appData => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "softwareSettings");
        public AuthWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string password = passwordField.Password.Trim();
            string login = loginField.Text.Trim();

            using (UserContext db = new UserContext())
            {
                var users = db.Users.Where(user => user.Login.Trim().Equals(login));
                
                foreach (User user in users)
                {
                    if (String.Equals(user.Login.Trim(), login) && String.Equals(user.Password.Trim(), password))
                    {
                        MessageBox.Show("Has been logged");
                        UserSer settings = new UserSer { Login = login };
                        string settingsText = JsonConvert.SerializeObject(settings);
                        File.WriteAllText(Path.Combine(
                            appData, "settings.json"), settingsText);
                        new MainWindow().Show();
                        this.Close();
                        return;
                    }
                }
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            new Regist().Show();
            this.Close();
        }
    }
}
