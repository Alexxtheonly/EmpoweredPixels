using System.Threading.Tasks;

namespace EmpoweredPixels.Jobs.Rewards
{
  public interface ILoginRewardJob
  {
    Task CreateLoginRewards();
  }
}
