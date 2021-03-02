using Playground.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class EnumSwitch
    {
        private List<EnumItem> _tabs;

        public ObservableCollection<string> Aliases { get; set; } = new ObservableCollection<string>();

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
            Aliases.CollectionChanged += AliasesOnCollectionChanged;
        }

        private void AliasesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (var i = 0; i < e.NewItems.Count; i++)
            {
                _tabs[e.NewStartingIndex + i].Label = (string)e.NewItems[i];
            }
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

            var values = Enum.GetValues(EnumType);
            _tabs = new List<EnumItem>();
            
            foreach (var val in values)
            {
                _tabs.Add(new EnumItem
                {
                    Label = val.ToString(),
                    Value = val
                });
            }

            ItemsSource = _tabs;
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

            SelectedItem = _tabs[SelectedIndex].Value;
        }

        private void UpdateIndex()
        {
            if (EnumType == null || _tabs == null || _tabs.Count == 0)
                return;

            if (SelectedItem == null)
            {
                SelectedIndex = -1;
                return;
            }

            SelectedIndex = _tabs.FindIndex(x => x.Value.Equals(SelectedItem));
        }
    }

    public class EnumItem : ObservableObject
    {
        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public object Value { get; set; }
    }
}