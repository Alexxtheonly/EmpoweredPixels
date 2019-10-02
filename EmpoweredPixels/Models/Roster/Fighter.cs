using System;
using System.Collections.Generic;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Ratings;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Models.Roster
{
  public class Fighter : IStats
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public long UserId { get; set; }

    public DateTimeOffset Created { get; set; }

    public bool IsDeleted { get; set; }

    public virtual FighterEloRating EloRating { get; set; }

    public virtual User User { get; set; }

    public int Power { get; set; }

    public int ConditionPower { get; set; }

    public int Precision { get; set; }

    public int Ferocity { get; set; }

    public int Accuracy { get; set; }

    public int Agility { get; set; }

    public int Armor { get; set; }

    public int Vitality { get; set; }

    public int HealingPower { get; set; }

    public int Speed { get; set; }

    public int Vision { get; set; }

    public int Level { get; set; }

    public ICollection<Equipment> Equipment { get; set; }

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

      if (Agility != other.Agility)
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
      return new { Id, Name, UserId, Accuracy, Speed, Power, Agility, Vitality, Vision, IsDeleted }.GetHashCode();
    }

    public override string ToString()
    {
      return $"{nameof(Id)}:{Id} {nameof(Name)}:{Name} {nameof(UserId)}:{UserId}";
    }
  }
}
