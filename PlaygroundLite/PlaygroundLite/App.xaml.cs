﻿using System;
using FreshMvvm;
using PlaygroundLite.Pages;
using PlaygroundLite.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlaygroundLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var page = FreshPageModelResolver.ResolvePageModel<MainViewModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;

            //MainPage = new MainView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
