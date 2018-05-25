using System;

using MagicBox.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MagicBox.Views
{
    public sealed partial class SignInPage : Page
    {
        private SignInViewModel ViewModel
        {
            get { return DataContext as SignInViewModel; }
        }

        public SignInPage()
        {
            InitializeComponent();
        }
    }
}
