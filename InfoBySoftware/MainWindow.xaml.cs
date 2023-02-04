using InfoBySoftware.Properties;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace InfoBySoftware
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string appData => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "softwareSettings");
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation= WindowStartupLocation.CenterScreen;

            UserSer settings = new UserSer();
            try
            {
                string settingsText = File.ReadAllText(Path.Combine(
                            appData, "settings.json"));
                settings = JsonConvert.DeserializeObject<UserSer>(settingsText);
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory(appData);
                File.Create(Path.Combine(appData, "settings.json"));
            }

            if(settings == null)
            {
                new AuthWindow().Show();
                this.Close();
            }
        }

        private void OutLogin_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(Path.Combine(
                appData, "settings.json"), null);
            new AuthWindow().Show();
            this.Close();
        }
    }
}
