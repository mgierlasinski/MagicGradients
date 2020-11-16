﻿using MagicGradients;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Playground.Features.Gallery.Models
{
    public class GradientEditorItem : INotifyPropertyChanged
    {
        public Gradient Data { get; set; }
        public string Type { get; set; }
        public IGradientSource GradientSource { get; set; }
        public List<GradientEditorStop> Stops { get; set; }

        private GradientEditorStop _selectedStop;
        public GradientEditorStop SelectedStop
        {
            get => _selectedStop;
            set
            {
                if (_selectedStop != value)
                {
                    _selectedStop = value;
                    OnPropertyChanged();
                }
            }
        }

        public static GradientEditorItem FromGradient(Gradient gradient)
        {
            gradient.Measure(100 ,100);

            var item = new GradientEditorItem
            {
                Data = gradient,
                GradientSource = new MagicGradients.LinearGradient {Angle = 270, Stops = gradient.Stops},
                Type = gradient.GetType().Name,
                Stops = gradient.Stops.Select(s => new GradientEditorStop
                {
                    Color = s.Color,
                    Offset = s.RenderOffset
                }).ToList(),
            };

            item.SelectedStop = item.Stops.FirstOrDefault();

            return item;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GradientEditorStop
    {
        public Color Color { get; set; }
        public double Offset { get; set; }
        public string ColorName => Color.ToHex();
        public string OffsetName => $"{Offset * 100}%";
        public Rectangle Bounds => new Rectangle(Offset, 0, 10, 1);
    }
}
