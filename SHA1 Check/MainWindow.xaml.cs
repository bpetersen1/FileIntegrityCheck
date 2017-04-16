using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SHA1_Check.sha1;
using SHA1_Check.md5;
using SHA1_Check.sha256;
using SHA1_Check.Common;

namespace SHA1_Check
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Sha1Length = 40;
        private const int Sha256Length = 64;

        public MainWindow()
        {
            InitializeComponent();
            List<string> cryptography = new SupportedCryptography().GetSupportedCryptography();
            DDLSupportedCryptography.ItemsSource = cryptography;
            DDLSupportedCryptography.SelectedItem = cryptography.FirstOrDefault();
        }

        private void OpenAboutWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private string BrowserWindowHelper()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            bool? result = dlg.ShowDialog();

            if (result != true) return string.Empty;

            return dlg.FileName;
        }

        #region Browse Buttons
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            txtFilePath.Text = BrowserWindowHelper();
        }
        private void Browsesha256_Click(object sender, RoutedEventArgs e)
        {
            txtFilePathsha256.Text = BrowserWindowHelper();
        }
        private void BrowseMd5_Click(object sender, RoutedEventArgs e)
        {
            TxtFilePathMd5.Text = BrowserWindowHelper();
        }
        private void Browsegenerate_Click(object sender, RoutedEventArgs e)
        {
            TxtFilePathgenerate.Text = BrowserWindowHelper();
        }
        #endregion

        #region Verify Buttons
        private async void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text) || string.IsNullOrWhiteSpace(TxtSha1Provided.Text)) return;
            lblresult.Content = "Working...";
            btnVerify.IsEnabled = false;
            TxtSha1Provided.IsReadOnly = true;

            GenerateSha1 sha1 = new GenerateSha1(txtFilePath.Text);
            string result = await sha1.GenerateFromFileAsync();
            string providedhash = TxtSha1Provided.Text.ToUpper();
            lblresult.Content = result.Equals(providedhash) ? "Verified" : "Error does not match";
            TxtSha1Provided.IsReadOnly = false;
            btnVerify.IsEnabled = true;
        }
        private async void BtnVerifyMd5_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFilePathMd5.Text) || string.IsNullOrWhiteSpace(TxtMd5Provided.Text)) return;
            LblresultMd5.Content = "Working...";
            BtnVerifyMd5.IsEnabled = false;
            TxtMd5Provided.IsReadOnly = true;

            GenerateMd5 md5 = new GenerateMd5(TxtFilePathMd5.Text);
            string result = await md5.GenerateFromFileAsync();
            string providedhash = TxtMd5Provided.Text.ToUpper();
            LblresultMd5.Content = result.Equals(providedhash) ? "Verified" : "Error does not match";
            TxtMd5Provided.IsReadOnly = false;
            BtnVerifyMd5.IsEnabled = true;
        }
        private async void btnVerifysha256_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePathsha256.Text) || string.IsNullOrWhiteSpace(Txtsha256Provided.Text)) return;
            lblresultsha256.Content = "Working...";
            btnVerifysha256.IsEnabled = false;
            Txtsha256Provided.IsReadOnly = true;

            GenerateSha256 sha256 = new GenerateSha256(txtFilePathsha256.Text);
            string result = await sha256.GenerateFromFileAsync();
            string providedhash = Txtsha256Provided.Text.ToUpper();
            lblresultsha256.Content = result.Equals(providedhash) ? "Verified" : "Error does not match";
            Txtsha256Provided.IsReadOnly = false;
            btnVerifysha256.IsEnabled = true;
        }

        private async void BtnVerifygenerate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFilePathgenerate.Text)) return;
            Lblresultgenerate.Content = "Working";
            BtnVerifygenerate.IsEnabled = false;
            TxtgenerateProvided.Text = string.Empty;
            switch (DDLSupportedCryptography.SelectedValue.ToString())
            {
                case "SHA-1":
                    TxtgenerateProvided.Text = await new GenerateSha1(TxtFilePathgenerate.Text).GenerateFromFileAsync();
                    break;
                case "SHA-256":
                    TxtgenerateProvided.Text = await new GenerateSha256(TxtFilePathgenerate.Text).GenerateFromFileAsync();
                    break;
                case "MD5":
                    TxtgenerateProvided.Text = await new GenerateMd5(TxtFilePathgenerate.Text).GenerateFromFileAsync();
                    break;
            }
            Lblresultgenerate.Content = string.Empty;
            BtnVerifygenerate.IsEnabled = true;
        }
        #endregion

        #region Events
        private void TxtSHA1Provided_OnTextChanged(object sender, TextChangedEventArgs e) => ValidateInputSha1Provided();
        private void TxtFilePath_OnTextChanged(object sender, TextChangedEventArgs e) => ValidateFileInputSha1();
        private void TxtMd5Provided_OnTextChanged(object sender, TextChangedEventArgs e) => ValidateInputMd5Provided();
        private void TxtFilePathMd5_OnTextChanged(object sender, TextChangedEventArgs e) => ValidateFileInputMd5();
        private void Txtsha256Provided_OnTextChanged(object sender, TextChangedEventArgs e) => ValidateInputSha256Provided();
        private void TxtFilePathsha256_OnTextChanged(object sender, TextChangedEventArgs e) => ValidateFileInputSha256();
        private void TxtFilePathgenerate_OnTextChanged(object sender, TextChangedEventArgs e)
            => BtnVerifygenerate.IsEnabled = !string.IsNullOrEmpty(TxtFilePathgenerate.Text);
        
        #endregion

        #region Validation
        #region SHA-1
        private void ValidateInputSha1Provided()
        {
            btnVerify.IsEnabled = (!string.IsNullOrEmpty(TxtSha1Provided?.Text) && TxtSha1Provided.Text.Length == Sha1Length && !string.IsNullOrWhiteSpace(txtFilePath.Text));
        }
        private void ValidateFileInputSha1()
        {
            btnVerify.IsEnabled = (!string.IsNullOrEmpty(TxtSha1Provided?.Text) && TxtSha1Provided.Text.Length == Sha1Length && !string.IsNullOrWhiteSpace(txtFilePath.Text));
        }
        #endregion
        #region SHA256
        private void ValidateInputSha256Provided()
        {
            btnVerifysha256.IsEnabled = (!string.IsNullOrEmpty(Txtsha256Provided?.Text) && Txtsha256Provided.Text.Length == Sha256Length && !string.IsNullOrWhiteSpace(txtFilePathsha256.Text));
        }
        private void ValidateFileInputSha256()
        {
            btnVerifysha256.IsEnabled = (!string.IsNullOrEmpty(Txtsha256Provided?.Text) && Txtsha256Provided.Text.Length == Sha256Length && !string.IsNullOrWhiteSpace(txtFilePathsha256.Text));
        }
        #endregion
        #region MD5
        private void ValidateInputMd5Provided()
        {
            BtnVerifyMd5.IsEnabled = (!string.IsNullOrEmpty(TxtMd5Provided?.Text) && !string.IsNullOrWhiteSpace(TxtFilePathMd5.Text));
        }
        private void ValidateFileInputMd5()
        {
            btnVerify.IsEnabled = (!string.IsNullOrEmpty(TxtMd5Provided?.Text) && !string.IsNullOrWhiteSpace(TxtFilePathMd5.Text));
        }
        #endregion

        #endregion

       

        
    }
}
