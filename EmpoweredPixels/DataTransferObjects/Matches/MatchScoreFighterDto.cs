﻿using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchScoreFighterDto
  {
    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public string Username { get; set; }

    public long UserId { get; set; }

    public int Points { get; set; }

    public Guid? TeamId { get; set; }

    public int TotalDamageDone { get; set; }

    public int TotalDamageTaken { get; set; }

    public int TotalEnergyUsed { get; set; }

    public int TotalAssists { get; set; }

    public int TotalKills { get; set; }

    public int TotalDeaths { get; set; }

    public double TotalDistanceTraveled { get; set; }

    public int TotalRegeneratedHealth { get; set; }

    public int TotalRegeneratedEnergy { get; set; }

    public int RoundsAlive { get; set; }
  }
}
