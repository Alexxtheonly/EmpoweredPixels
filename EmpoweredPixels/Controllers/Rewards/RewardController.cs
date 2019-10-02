using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.DataTransferObjects.Rewards;
using EmpoweredPixels.Exceptions.Rewards;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Rewards;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Rewards;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Rewards
{
  public class RewardController : ControllerBase<DatabaseContext, RewardController>
  {
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IRewardFactory rewardFactory;

    public RewardController(DatabaseContext context, ILogger<RewardController> logger, IMapper mapper, IDateTimeProvider dateTimeProvider, IRewardFactory rewardFactory)
      : base(context, logger, mapper)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.rewardFactory = rewardFactory;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RewardDto>>> GetRewards()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var loginRewards = await Context.Rewards
        .Where(o => o.UserId == userId)
        .Where(o => o.Claimed == null)
        .ProjectTo<RewardDto>(Mapper.ConfigurationProvider)
        .ToListAsync();

      return Ok(loginRewards);
    }

    [HttpPost("claim")]
    public async Task<ActionResult<RewardContentDto>> ClaimReward([FromBody] RewardDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var reward = await Context.Rewards
        .AsTracking()
        .Where(o => o.Claimed == null)
        .FirstOrDefaultAsync(o => o.Id == dto.Id && o.RewardPoolId == dto.PoolId && o.UserId == userId);

      if (reward == null)
      {
        return BadRequest(new InvalidRewardException());
      }

      reward.Claimed = dateTimeProvider.Now;

      var items = rewardFactory.Claim(reward).ToList();
      foreach (var item in items)
      {
        Context.Add(item);
      }

      await Context.SaveChangesAsync();

      var content = new RewardContentDto()
      {
        Items = Mapper.Map<ICollection<ItemDto>>(items.OfType<Item>()),
        Equipment = Mapper.Map<ICollection<EquipmentDto>>(items.OfType<Equipment>()),
      };

      return Ok(content);
    }

    [HttpPost("claim/all")]
    public async Task<ActionResult<RewardContentDto>> ClaimAll()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var rewards = await Context.Rewards
        .AsTracking()
        .Where(o => o.Claimed == null && o.UserId == userId)
        .ToListAsync();

      if (!rewards.Any())
      {
        return BadRequest();
      }

      var rewarded = new List<IReward>();
      foreach (var reward in rewards)
      {
        reward.Claimed = dateTimeProvider.Now;
        var items = rewardFactory.Claim(reward).ToList();
        rewarded.AddRange(items);

        foreach (var item in items)
        {
          Context.Add(item);
        }
      }

      await Context.SaveChangesAsync();

      var content = new RewardContentDto()
      {
        Items = Mapper.Map<ICollection<ItemDto>>(rewarded.OfType<Item>()),
        Equipment = Mapper.Map<ICollection<EquipmentDto>>(rewarded.OfType<Equipment>()),
      };

      return Ok(content);
    }
  }
}
