using System;
using EmpoweredPixels.Models.Identity;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Models.Roster
{
  public class Fighter : IStats
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public long UserId { get; set; }

    public DateTimeOffset Created { get; set; }

    public float Accuracy { get; set; }

    public float Power { get; set; }

    public float Expertise { get; set; }

    public float Agility { get; set; }

    public float Toughness { get; set; }

    public float Vitality { get; set; }

    public float Speed { get; set; }

    public float Stamina { get; set; }

    public float Regeneration { get; set; }

    public float Vision { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User User { get; set; }

    public IStats Clone()
    {
      return this;
    }

    public bool Equals(Fighter other)
    {
      if (other == null)
      {
        return false;
      }

      if (Id != other.Id)
      {
        return false;
      }

      if (!string.Equals(Name, other.Name))
      {
        return false;
      }

      if (UserId != other.UserId)
      {
        return false;
      }

      if (Created != other.Created)
      {
        return false;
      }

      if (Accuracy != other.Accuracy)
      {
        return false;
      }

      if (Power != other.Power)
      {
        return false;
      }

      if (Expertise != other.Expertise)
      {
        return false;
      }

      if (Agility != other.Agility)
      {
        return false;
      }

      if (Toughness != other.Toughness)
      {
        return false;
      }

      if (Vitality != other.Vitality)
      {
        return false;
      }

      if (Speed != other.Speed)
      {
        return false;
      }

      if (Stamina != other.Stamina)
      {
        return false;
      }

      if (Regeneration != other.Regeneration)
      {
        return false;
      }

      if (Vision != other.Vision)
      {
        return false;
      }

      if (IsDeleted != other.IsDeleted)
      {
        return false;
      }

      return true;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as Fighter);
    }

    public override int GetHashCode()
    {
      return new { Id, Name, UserId, Accuracy, Speed, Power, Expertise, Agility, Toughness, Vitality, Stamina, Regeneration, Vision, IsDeleted }.GetHashCode();
    }

    public override string ToString()
    {
      return $"{nameof(Id)}:{Id} {nameof(Name)}:{Name} {nameof(UserId)}:{UserId}";
    }
  }
}
