using Bogus;
using MagicGradients;
using MagicGradients.Parser;
using Playground.Constants;
using Playground.Data.Repositories;
using Playground.Models;
using Playground.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Xamarin.Forms;
using static Playground.Constants.IconCodes;
using Color = System.Drawing.Color;

namespace Playground.ViewModels
{
    [QueryProperty("Id", "id")]
    public class BattleTestViewModel : BaseViewModel
    {
        private readonly IGradientRepository _gradientRepository;
        private readonly IPickerColorsDataProvider _pickerColorsDataProvider;

        private string _cssCode;
        
        private List<BattleItem> _iconsCollection;
        public List<BattleItem> IconsCollection
        {
            get => _iconsCollection;
            set => SetProperty(ref _iconsCollection, value);
        }

        private List<BattleItem> _itemsCollection;
        public List<BattleItem> ItemsCollection
        {
            get => _itemsCollection;
            set => SetProperty(ref _itemsCollection, value);
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                LoadCssCodeById();
            }
        }

        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set
            {
                if (SetProperty(ref _textColor, value))
                {
                    IconsCollection = GenerateIconsCollection();
                    ItemsCollection = GenerateItemsCollection();
                }
            }
        }

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value);
        }

        private int _selectedColorIndex;
        public int SelectedColorIndex
        {
            get => _selectedColorIndex;
            set 
            {
                if(SetProperty(ref _selectedColorIndex, value))
                {
                    TextColor = _pickerColorsDataProvider.GetColorByName(ColorNames[SelectedColorIndex]);
                }
            }
        }

        public List<string> ColorNames { get; } 

        public BattleTestViewModel(IGradientRepository gradientRepository, IPickerColorsDataProvider pickerColorsDataProvider)
        {
            _gradientRepository = gradientRepository;
            _pickerColorsDataProvider = pickerColorsDataProvider;
            
            ColorNames = _pickerColorsDataProvider.GetColorNames();
            TextColor = Color.White;
        }

        private void LoadCssCodeById()
        {
            var gradient = _gradientRepository.GetById(int.Parse(_id));

            if (gradient == null)
                return;

            _cssCode = gradient.Stylesheet;
            UpdateGradientSource();
        }

        private void UpdateGradientSource()
        {
            try
            {
                var parser = new CssGradientParser();
                var gradients = parser.ParseCss(_cssCode);

                GradientSource = new GradientCollection
                {
                    Gradients = new ObservableCollection<Gradient>(gradients)
                };
                IconsCollection = GenerateIconsCollection();
            }
            catch (Exception e)
            {
                //todo maybe later we should add some dialog or toast service?
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                Debugger.Break();
            }
        }

        private List<BattleItem> GenerateIconsCollection() => new List<BattleItem>
        {
            new BattleItem {GradientSource = GradientSource, Text = MagicWand, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Refresh, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = IconCodes.Gradient, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Radial, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Palette, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Layers, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Gallery, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Code, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Bolt, TextColor = TextColor},
            new BattleItem {GradientSource = GradientSource, Text = Paint, TextColor = TextColor}
        };

        private List<BattleItem> GenerateItemsCollection()
        {
            var battleList = new List<BattleItem>(90);
            var fackedBattleItem = new Faker<BattleItem>()
                .RuleFor(item => item.Text, (facker, item) => facker.Name.LastName())
                .RuleFor(item => item.TextColor, (facker) => TextColor);

            battleList.AddRange(
                Enumerable.Range(0, 90)
                          .Select(_ => fackedBattleItem.Generate())
                );

            return battleList;
        }
    }
}
