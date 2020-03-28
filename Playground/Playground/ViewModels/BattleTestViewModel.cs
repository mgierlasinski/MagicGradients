using Playground.Data.Repositories;
using Xamarin.Forms;
using System.Diagnostics;
using MagicGradients.Parser;
using MagicGradients;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using Playground.Constants;

using static Playground.Constants.IconCodes;

namespace Playground.ViewModels
{
    [QueryProperty("Id", "id")]
    public class BattleTestViewModel : BaseViewModel
    {
        private readonly IGradientRepository _gradientRepository;
        
        private string _cssCode;

        public List<string> IconsCollection { get; } = new List<string>()
        {
            Bolt, Code, MagicWand, Refresh,
            Layers, Bolt, Paint
        };

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

        private IGradientSource _gradientSource;
        public IGradientSource GradientSource
        {
            get => _gradientSource;
            set => SetProperty(ref _gradientSource, value);
        }

        public BattleTestViewModel(IGradientRepository gradientRepository)
        {
            _gradientRepository = gradientRepository;
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
            }
            catch (Exception e)
            {
                //todo maybe later we should add some dialog or toast service?
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                Debugger.Break();
            }
        }
    }
}
