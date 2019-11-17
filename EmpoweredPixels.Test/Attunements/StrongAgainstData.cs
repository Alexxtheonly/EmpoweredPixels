using EmpoweredPixels.Attunements;

namespace EmpoweredPixels.Test.Attunements
{
  public class StrongAgainstData : TheoryData
  {
    public StrongAgainstData()
    {
      AddRow(new FireAttunement(), new WindAttunement());
      AddRow(new WindAttunement(), new LightningAttunement());
      AddRow(new LightningAttunement(), new EarthAttunement());
      AddRow(new EarthAttunement(), new WaterAttunement());
      AddRow(new WaterAttunement(), new FireAttunement());
    }
  }
}
