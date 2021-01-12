using Sharpnado.Tabs;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Playground.Controls
{
    public partial class EnumSwitch : TabHostView
    {
        private string[] _enumItems;

        public static readonly BindableProperty EnumTypeProperty = BindableProperty.Create(nameof(EnumType),
            typeof(Type), typeof(EnumSwitch), propertyChanged: OnEnumTypeChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(object), typeof(EnumSwitch), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

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

        private static void OnEnumTypeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((EnumSwitch)bindable).GenerateTabs();
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((EnumSwitch)bindable).UpdateIndex();
        }

        public EnumSwitch()
        {
            InitializeComponent();
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
            while (Tabs.Count > 0)
            {
                Tabs.RemoveAt(0);
            }

            if (EnumType == null)
                return;

            _enumItems = Enum.GetNames(EnumType);

            foreach (var name in _enumItems)
            {
                var tab = new SegmentedTabItem { Label = name };
                Tabs.Add(tab);
            }
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

            SelectedItem = Enum.ToObject(EnumType, SelectedIndex);
        }

        private void UpdateIndex()
        {
            if (EnumType == null || _enumItems == null || !_enumItems.Any())
                return;

            if (SelectedItem == null)
            {
                SelectedIndex = -1;
                return;
            }

            var name = Enum.GetName(EnumType, SelectedItem);
            var index = _enumItems.IndexOf(name);

            SelectedIndex = index;
        }
    }
}