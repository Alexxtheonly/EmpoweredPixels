using EmpoweredPixels.Attunements;

namespace EmpoweredPixels.Test.Attunements
{
  public class NeutralAgainstData : TheoryData
  {
    public NeutralAgainstData()
    {
      AddRow(new FireAttunement(), new LightningAttunement());
      AddRow(new FireAttunement(), new EarthAttunement());

      AddRow(new WindAttunement(), new EarthAttunement());
      AddRow(new WindAttunement(), new WaterAttunement());

      AddRow(new LightningAttunement(), new WaterAttunement());
      AddRow(new LightningAttunement(), new FireAttunement());

      AddRow(new EarthAttunement(), new FireAttunement());
      AddRow(new EarthAttunement(), new WindAttunement());

      AddRow(new WaterAttunement(), new WindAttunement());
      AddRow(new WaterAttunement(), new LightningAttunement());
    }
  }
}
