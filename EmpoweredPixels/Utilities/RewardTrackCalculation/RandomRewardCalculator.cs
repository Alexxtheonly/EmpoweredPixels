using System;
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
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly DatabaseContext context;

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
      if (1.25F.Chance())
      {
        return EmpoweredChestRewardPool.Mythic;
      }

      if (8F.Chance())
      {
        return EmpoweredChestRewardPool.Fabled;
      }

      if (30F.Chance())
      {
        return EmpoweredChestRewardPool.Rare;
      }

      if (50F.Chance())
      {
        return EmpoweredChestRewardPool.Common;
      }

      return null;
    }

    private int GetRolls()
    {
      if (1F.Chance())
      {
        return 5;
      }

      if (4F.Chance())
      {
        return 4;
      }

      if (8F.Chance())
      {
        return 3;
      }

      if (12F.Chance())
      {
        return 2;
      }

      return 1;
    }
  }
}
