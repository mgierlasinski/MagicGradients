using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class RadialMenu : Grid
    {
        public List<RadialMenuItem> Items = new List<RadialMenuItem>()
        {
            new RadialMenuItem { Background = Color.FromHex("#eb4141") },
            new RadialMenuItem { Background = Color.FromHex("#eb9641") },
            new RadialMenuItem { Background = Color.FromHex("#e8eb41") },
            new RadialMenuItem { Background = Color.FromHex("#4deb41") },
            new RadialMenuItem { Background = Color.FromHex("#41bbeb") },
            new RadialMenuItem { Background = Color.FromHex("#6641eb") },
            new RadialMenuItem { Background = Color.FromHex("#e241eb") }
        };

        public RadialMenu()
        {
            InitializeComponent();
        }

        public void UnselectCurrentItem()
        {
            var selected = Items.FirstOrDefault(x => x.IsSelected);
            if (selected != null)
            {
                selected.IsSelected = false;
            }
        }
    }
}