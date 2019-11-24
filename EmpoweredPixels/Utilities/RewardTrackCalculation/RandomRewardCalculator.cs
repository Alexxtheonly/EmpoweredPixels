﻿using System;
using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Rewards.Pools.Chests;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Utilities.RewardTrackCalculation
{
  public class RandomRewardCalculator : IRewardTrackCalculator
  {
    private IDateTimeProvider dateTimeProvider;
    private DatabaseContext context;

    public RandomRewardCalculator(IDateTimeProvider dateTimeProvider, DatabaseContext context)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.context = context;
    }

    public async Task Calculate(Fighter fighter, int points)
    {
      var level = fighter.Level.NearestBase(8);
      if (level == 0)
      {
        level = 1;
      }

      for (int i = 0; i < GetRolls(); i++)
      {
        var poolId = GetRewardPoolId();
        if (poolId == null)
        {
          continue;
        }

        context.Rewards.Add(new Reward()
        {
          UserId = fighter.UserId,
          Level = level,
          Created = dateTimeProvider.Now,
          RewardPoolId = poolId.Value,
        });
      }

      await context.SaveChangesAsync();
    }

    private Guid? GetRewardPoolId()
    {
      if (10F.Chance())
      {
        return EmpoweredChestRewardPool.Mythic;
      }

      if (25F.Chance())
      {
        return EmpoweredChestRewardPool.Fabled;
      }

      if (50F.Chance())
      {
        return EmpoweredChestRewardPool.Rare;
      }

      if (80F.Chance())
      {
        return EmpoweredChestRewardPool.Common;
      }

      return null;
    }

    private int GetRolls()
    {
      if (5F.Chance())
      {
        return 5;
      }

      if (15F.Chance())
      {
        return 4;
      }

      if (25F.Chance())
      {
        return 3;
      }

      if (35F.Chance())
      {
        return 2;
      }

      return 1;
    }
  }
}
