﻿using Xamarin.Forms;

namespace Playground.Features.Masks
{
    public partial class MasksPage : ContentPage
    {
        public MasksPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            AngleAnimation.End();
        }
    }
}