using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.DataTransferObjects.Rewards;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Rewards;
using EmpoweredPixels.Models;
using EmpoweredPixels.Providers.DateTime;
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
    public async Task<ActionResult<IEnumerable<ItemDto>>> ClaimReward([FromBody] RewardDto dto)
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
        return BadRequest();
      }

      reward.Claimed = dateTimeProvider.Now;

      var items = rewardFactory.Claim(reward).ToList();
      foreach (var item in items)
      {
        Context.Add(item);
      }

      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<IEnumerable<ItemDto>>(items));
    }
  }
}
