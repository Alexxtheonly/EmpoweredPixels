using EmpoweredPixels.Interfaces.Equipment;

namespace EmpoweredPixels.Utilities.EnhancementCalculation
{
  public interface IEnhancementProbability
  {
    bool IsSuccess(IEnhancable enhancable);
  }
}
