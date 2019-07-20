using System.Threading.Tasks;
using EmpoweredPixels.DataTransferObjects.Matches;
using Microsoft.AspNetCore.SignalR;

namespace EmpoweredPixels.Hubs.Matches
{
  public class MatchHub : Hub<IMatchClient>
  {
    public Task JoinGroup(string group)
    {
      return Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public Task LeaveGroup(string group)
    {
      return Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }

    public Task UpdateMatch(MatchDto matchDto)
    {
      return Clients.Group(matchDto.Id.ToString()).UpdateMatch(matchDto);
    }
  }
}
