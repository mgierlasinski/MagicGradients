﻿using Bogus;
using Playground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.Services
{
    public interface IBattleItemService
    {
        List<BattleItem> GenerateItems(Faker<BattleItem> facker, int count);
    }
    public class BattleItemService : IBattleItemService
    {
        public List<BattleItem> GenerateItems(Faker<BattleItem> facker, int count)
        {
            var battleList = new List<BattleItem>(count);

            battleList.AddRange(
                Enumerable.Range(0, count)
                          .Select(_ => facker.Generate())
                );

            return battleList;
        }
    }
}
