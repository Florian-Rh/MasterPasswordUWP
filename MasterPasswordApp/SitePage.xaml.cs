using MasterPasswordApp.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MasterPasswordApp.ViewModel;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MasterPasswordApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SitePage : Page
    {
        public SiteViewModel viewModel { get; set; }
        public string userName { get; set; } 
        private byte[] seed { get; set; }

        private static SitePage instance;
        public SitePage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().IsScreenCaptureEnabled = false;
            hider.Visibility = Visibility.Collapsed;
            if (MasterKey.Exists)
            {
                viewModel = new SiteViewModel();
                viewModel.count = 1;
                viewModel.siteType = (int)SiteType.MediumPassword;
                StatusBar.GetForCurrentView().HideAsync();
            }
            else
            {
                Frame.Navigate(typeof(LoginPage));
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            userName = e.Parameter.ToString();
            this.DataContext = this;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        private void btnLogOff_Click(object sender, RoutedEventArgs e)
        {
            MasterKey.Destroy();
            Frame.Navigate(typeof(LoginPage));
        }

        private void btnCalculatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.siteName))
            {
                MasterKey masterKey = MasterKey.GetMasterKey();
                seed = masterKey.GetPasswordSeed(viewModel.siteName, viewModel.count);
                viewModel.password = masterKey.GetPassword(seed, (SiteType)viewModel.siteType);
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            DataPackage data = new DataPackage();
            data.RequestedOperation = DataPackageOperation.Copy;
            data.SetText(viewModel.password);
            Clipboard.SetContent(data);
        }

        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            viewModel.count++;
        }

        private void btnLess_Click(object sender, RoutedEventArgs e)
        {
            viewModel.count--;
        }
       

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.showPassword = !viewModel.showPassword;
        }
    }
}
