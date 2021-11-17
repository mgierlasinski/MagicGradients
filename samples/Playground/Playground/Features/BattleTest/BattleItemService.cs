using Bogus;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Playground.Features.BattleTest
{
    public interface IBattleItemService
    {
        List<BattleItem> GenerateItems(Faker<BattleItem> faker, int count);
        List<string> GetColorNames();
        Color GetColorByName(string colorName);
    }

    public class BattleItemService : IBattleItemService
    {
        private Dictionary<string, Color> NamesToColors { get; } = new Dictionary<string, Color>
        {
            {"White", Color.White},
            {"Black", Color.Black}
        };
        
        public List<BattleItem> GenerateItems(Faker<BattleItem> faker, int count)
        {
            var battleList = new List<BattleItem>(count);

            battleList.AddRange(
                Enumerable.Range(0, count)
                          .Select(_ => faker.Generate())
                );

            return battleList;
        }

        public Color GetColorByName(string colorName) => NamesToColors[colorName];

        public List<string> GetColorNames() => NamesToColors.Keys.ToList();
    }
}
