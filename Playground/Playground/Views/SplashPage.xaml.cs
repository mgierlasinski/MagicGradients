using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Playground.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            
            IoC.RegisterTypes();
            IoC.Initialize();

            Application.Current.MainPage = new AppShell();
        }
    }
}