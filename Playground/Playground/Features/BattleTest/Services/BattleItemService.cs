using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Features.BattleTest.Services
{
    public interface IBattleItemService
    {
        List<BattleItem> GenerateItems(Faker<BattleItem> faker, int count);
    }

    public class BattleItemService : IBattleItemService
    {
        public List<BattleItem> GenerateItems(Faker<BattleItem> faker, int count)
        {
            var battleList = new List<BattleItem>(count);

            battleList.AddRange(
                Enumerable.Range(0, count)
                          .Select(_ => faker.Generate())
                );

            return battleList;
        }
    }
}
