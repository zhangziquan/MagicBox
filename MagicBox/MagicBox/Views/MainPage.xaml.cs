using System;

using MagicBox.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MagicBox.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
