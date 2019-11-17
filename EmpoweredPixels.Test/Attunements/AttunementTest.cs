using EmpoweredPixels.Attunements;
using Xunit;

namespace EmpoweredPixels.Test.Attunements
{
  public class AttunementTest
  {
    [Theory]
    [ClassData(typeof(StrongAgainstData))]
    public void ShouldBeStrongAgainst(AttunementBase strong, AttunementBase weak)
    {
      Assert.Equal(strong.StrongAgainstAttunementId, weak.Id);
      Assert.Equal(weak.WeakAgainstAttunementId, strong.Id);
    }

    [Theory]
    [ClassData(typeof(NeutralAgainstData))]
    public void ShouldBeNeutralAgainst(AttunementBase left, AttunementBase right)
    {
      Assert.NotEqual(right.WeakAgainstAttunementId, left.Id);
      Assert.NotEqual(right.StrongAgainstAttunementId, left.Id);

      Assert.NotEqual(left.WeakAgainstAttunementId, right.Id);
      Assert.NotEqual(left.StrongAgainstAttunementId, right.Id);
    }

    [Theory]
    [ClassData(typeof(StrongAgainstData))]
    public void ShouldDealMoreDamageAgainst(AttunementBase strong, AttunementBase weak)
    {
      var damage = 100;

      Assert.True(strong.CalculateDamageDone(weak, damage) > damage);
    }

    [Theory]
    [ClassData(typeof(StrongAgainstData))]
    public void ShouldDealLessDamageAgainst(AttunementBase strong, AttunementBase weak)
    {
      var damage = 100;

      Assert.True(weak.CalculateDamageDone(strong, damage) < damage);
    }

    [Theory]
    [ClassData(typeof(NeutralAgainstData))]
    public void ShouldDealNeutralDamageAgainst(AttunementBase left, AttunementBase right)
    {
      var damage = 100;

      Assert.Equal(damage, left.CalculateDamageDone(right, damage));
      Assert.Equal(damage, right.CalculateDamageDone(left, damage));
    }
  }
}
