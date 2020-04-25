using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class RadialMenu : Grid
    {
        private readonly Picker _internalPicker = new Picker();

        public IList<string> Items => _internalPicker.Items;

        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(RadialMenu), false, BindingMode.TwoWay,
                propertyChanged: OnIsOpenChanged);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(RadialMenu), default(IList),
                propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RadialMenu), -1, BindingMode.TwoWay, 
                propertyChanged: OnSelectedIndexChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RadialMenu), null, BindingMode.TwoWay, 
                propertyChanged: OnSelectedItemChanged);

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public RadialMenu()
        {
            InitializeComponent();
            InitializeHidden();

            _internalPicker.PropertyChanged += InternalPickerOnPropertyChanged;
        }

        private void InternalPickerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Picker.SelectedIndex))
            {
                SelectedIndex = _internalPicker.SelectedIndex;
            }
            else if (e.PropertyName == nameof(Picker.SelectedItem))
            {
                SelectedItem = _internalPicker.SelectedItem;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetInheritedBindingContext(_internalPicker, BindingContext);
        }

        private static void OnIsOpenChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((RadialMenu)bindable).SetVisibility((bool)newvalue);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((RadialMenu)bindable)._internalPicker.ItemsSource = (IList)newvalue;
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((RadialMenu)bindable)._internalPicker.SelectedIndex = (int)newvalue;
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((RadialMenu)bindable)._internalPicker.SelectedItem = newvalue;
        }

        private void SetVisibility(bool isOpen)
        {
            if (isOpen)
            {
                OpenMenu();
            }
            else
            {
                HideMenu();
            }
        }

        private async Task OpenMenu()
        {
            IsVisible = true;

            Overlay.FadeTo(0.6, 300);

            await CenterButton.ScaleTo(1, 300);

            await Task.WhenAll(
                CircleMenu.ScaleTo(1, 300, Easing.CubicOut),
                CircleMenu.RotateTo(360, 300));

            await ClearButton.ScaleTo(1, 300, Easing.CubicOut);
        }

        private async Task HideMenu()
        {
            Overlay.FadeTo(0, 300);

            await Task.WhenAll(
                CircleMenu.ScaleTo(0, 300),
                CircleMenu.RelRotateTo(-360, 300),
                ClearButton.ScaleTo(0, 300)); 
            
            await CenterButton.ScaleTo(0, 300);

            IsVisible = false;
        }

        private void InitializeHidden()
        {
            IsVisible = false;
            Overlay.Opacity = 0;
            CircleMenu.Scale = 0;
            CenterButton.Scale = 0;
            ClearButton.Scale = 0;
        }

        private void CenterButton_Tapped(object sender, EventArgs e)
        {
            IsOpen = false;
        }

        private void ClearButton_Tapped(object sender, EventArgs e)
        {
            SelectedItem = null;
            SelectedIndex = -1;
            IsOpen = false;
        }
    }
}