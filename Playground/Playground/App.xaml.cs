﻿using Xamarin.Forms;

namespace Playground
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "Expander_Experimental", "Shell_UWP_Experimental" });

            InitializeComponent();

            IoC.RegisterTypes();
            IoC.Initialize();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
