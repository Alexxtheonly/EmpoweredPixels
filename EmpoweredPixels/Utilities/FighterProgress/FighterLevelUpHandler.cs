using System;
using System.Collections.Generic;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Rewards.Pools.Chests;
using EmpoweredPixels.Rewards.Pools.FighterLevelUp;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Utilities.FighterProgress
{
  public class FighterLevelUpHandler : IFighterLevelUpHandler
  {
    private readonly IDateTimeProvider dateTimeProvider;

    public FighterLevelUpHandler(IDateTimeProvider dateTimeProvider)
    {
      this.dateTimeProvider = dateTimeProvider;
    }

    public IEnumerable<Reward> Up(Fighter fighter)
    {
      fighter.Level += 1;

      const int baseValue = 10;

      fighter.Power = baseValue * fighter.Level;
      fighter.Armor = baseValue * fighter.Level;
      fighter.Accuracy = 3 * fighter.Level;
      fighter.Agility = 3 * fighter.Level;
      fighter.ConditionPower = baseValue * fighter.Level;
      fighter.Ferocity = 50;
      fighter.HealingPower = baseValue * fighter.Level;
      fighter.Precision = 20;
      fighter.Speed = 10;
      fighter.Vision = 10;
      fighter.Vitality = baseValue * fighter.Level;

      var rewards = new List<Reward>();

      var level = fighter.Level.NearestBase(8);
      if (level == 0)
      {
        level = 1;
      }

      var isNewItemsetLevel = fighter.Level % 8 == 0;
      if (isNewItemsetLevel)
      {
        rewards.Add(new Reward()
        {
          Level = level,
          RewardPoolId = FighterLevelRewardPool.Id,
          UserId = fighter.UserId,
          Created = dateTimeProvider.Now,
        });
      }

      rewards.Add(new Reward()
      {
        Level = level,
        RewardPoolId = GetChestPoolId(),
        UserId = fighter.UserId,
        Created = dateTimeProvider.Now,
      });

      return rewards;
    }

    private Guid GetChestPoolId()
    {
      if (1F.Chance())
      {
        return EmpoweredChestRewardPool.Mythic;
      }

      if (10F.Chance())
      {
        return EmpoweredChestRewardPool.Fabled;
      }

      if (50F.Chance())
      {
        return EmpoweredChestRewardPool.Rare;
      }

      return EmpoweredChestRewardPool.Common;
    }
  }
}
