using System.Threading.Tasks;
using EmpoweredPixels.DataTransferObjects.Matches;

namespace EmpoweredPixels.Hubs.Matches
{
  public interface IMatchClient
  {
    Task JoinGroup(string group);

    Task LeaveGroup(string group);

    Task UpdateMatch(MatchDto matchDto);
  }
}
