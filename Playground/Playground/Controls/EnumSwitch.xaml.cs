using Sharpnado.Tabs;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Playground.Controls
{
    public partial class EnumSwitch : TabHostView
    {
        private Array _enumItems;

        public static readonly BindableProperty EnumTypeProperty = BindableProperty.Create(nameof(EnumType),
            typeof(Type), typeof(EnumSwitch), propertyChanged: OnEnumTypeChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(object), typeof(EnumSwitch), defaultBindingMode: BindingMode.TwoWay, 
            propertyChanged: (bindable, value, newValue) => ((EnumSwitch)bindable).OnSelectedItemChanged());

        public Type EnumType
        {
            get => (Type)GetValue(EnumTypeProperty);
            set => SetValue(EnumTypeProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public event EventHandler SelectedItemChanged;

        public EnumSwitch()
        {
            InitializeComponent();
        }

        private static void OnEnumTypeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((EnumSwitch)bindable).GenerateTabs();
        }

        private void OnSelectedItemChanged()
        {
            UpdateIndex();
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SelectedIndex))
            {
                UpdateItem();
            }
        }

        private void GenerateTabs()
        {
            if (EnumType == null)
                return;

            _enumItems = Enum.GetValues(EnumType);
            ItemsSource = _enumItems;
        }

        private void UpdateItem()
        {
            if (EnumType == null)
                return;

            if (SelectedIndex == -1)
            {
                SelectedItem = null;
                return;
            }

            SelectedItem = _enumItems.GetValue(SelectedIndex);
        }

        private void UpdateIndex()
        {
            if (EnumType == null || _enumItems == null || _enumItems.Length == 0)
                return;

            if (SelectedItem == null)
            {
                SelectedIndex = -1;
                return;
            }

            SelectedIndex = Array.IndexOf(_enumItems, SelectedItem);
        }
    }
}