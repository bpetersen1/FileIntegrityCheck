using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;


namespace SHA1_Check
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

            this.Title = $"SHA-1 Check v{versionInfo.FileVersion}";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
