﻿namespace PlaygroundMaui.Pages
{
    public partial class AnimationsPage
    {
        public AnimationsPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            ColorAnimation.End();
        }
    }
}