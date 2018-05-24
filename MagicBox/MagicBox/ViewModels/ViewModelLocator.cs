using System;

using CommonServiceLocator;

using GalaSoft.MvvmLight.Ioc;

using MagicBox.Services;
using MagicBox.Views;

namespace MagicBox.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<BlankViewModel, BlankPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public BlankViewModel BlankViewModel => ServiceLocator.Current.GetInstance<BlankViewModel>();

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
