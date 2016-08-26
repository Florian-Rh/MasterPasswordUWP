using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MasterPasswordApp.Crypto;
using MasterPasswordApp.ViewModel;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MasterPasswordApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel viewModel { get; set; }
        public LoginPage()
        {
            this.InitializeComponent();
            viewModel = new LoginViewModel();
            viewModel.EditIsEnabled = true;
            this.DataContext = this;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.MasterPassword) && !String.IsNullOrEmpty(viewModel.UserName))
            {
                viewModel.EditIsEnabled = false;
                StatusBarProgressIndicator statusBarIndicator = StatusBar.GetForCurrentView().ProgressIndicator;
                StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.White;
                statusBarIndicator.Text = "Master Key is derived...";
        
                await statusBarIndicator.ShowAsync();
                await MasterKey.CreateMasterKeyAsync(viewModel.MasterPassword, viewModel.UserName);
                StatusBar.GetForCurrentView().BackgroundColor = Windows.UI.Colors.Green;
                statusBarIndicator.Text = "Derivation complete! Login successful";
                Frame.Navigate(typeof(SitePage), viewModel.UserName);
            }
        }
    }
}
