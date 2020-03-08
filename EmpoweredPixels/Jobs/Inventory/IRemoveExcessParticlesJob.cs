using System.Threading.Tasks;

namespace EmpoweredPixels.Jobs.Inventory
{
  public interface IRemoveExcessParticlesJob
  {
    Task RemoveAsync();
  }
}
