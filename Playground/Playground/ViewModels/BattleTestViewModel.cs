using Bogus;
using MagicGradients;
using Playground.Constants;
using Playground.Data.Repositories;
using Playground.Models;
using Playground.Services;
using System.Collections.Generic;
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
        private readonly IBattleItemService _battleItemService;
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
        public string Id //Don't remove it we set it from query
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
                if (SetProperty(ref _selectedColorIndex, value))
                {
                    TextColor = _pickerColorsDataProvider.GetColorByName(ColorNames[SelectedColorIndex]);
                }
            }
        }

        public List<string> ColorNames { get; }

        public BattleTestViewModel(
            IGradientRepository gradientRepository, 
            IPickerColorsDataProvider pickerColorsDataProvider,
            IBattleItemService battleItemService)
        {
            _gradientRepository = gradientRepository;
            _pickerColorsDataProvider = pickerColorsDataProvider;
            _battleItemService = battleItemService;

            ColorNames = _pickerColorsDataProvider.GetColorNames();
            TextColor = Color.White;
        }

        private void LoadCssCodeById()
        {
            var gradient = _gradientRepository.GetById(int.Parse(Id));
            GradientSource = new CssGradientSource { Stylesheet = gradient.Stylesheet };
            IconsCollection = GenerateIconsCollection();
        }

        private List<BattleItem> GenerateIconsCollection()
        {
            var iconsCodeList = new List<string>{
               MagicWand, Refresh, IconCodes.Gradient, Radial,
               Palette, Layers, Gallery, Code, Bolt, Paint
           };
            var fackedBattleItem = new Faker<BattleItem>()
                .RuleFor(item => item.Text, (faker) => faker.PickRandom(iconsCodeList))
                .RuleFor(item => item.TextColor, (faker) => TextColor)
                .RuleFor(item => item.GradientSource, (faker) => GradientSource);

            return _battleItemService.GenerateItems(fackedBattleItem, 15);
        }

        private List<BattleItem> GenerateItemsCollection()
        {
            var fackedBattleItem = new Faker<BattleItem>()
                .RuleFor(item => item.Text, (faker) => faker.Name.LastName())
                .RuleFor(item => item.TextColor, (faker) => TextColor);

            return _battleItemService.GenerateItems(fackedBattleItem, 90);
        }
    }
}
