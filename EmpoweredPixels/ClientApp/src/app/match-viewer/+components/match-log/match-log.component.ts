import { Component, OnInit, Input, ChangeDetectionStrategy } from '@angular/core';
import { RoundTick } from '../../+models/round-tick';
import { RosterService } from 'src/app/roster/+services/roster.service';
import { FighterSpawnedTick } from '../../+models/fighter-spawned-tick';
import { GameFighter } from 'src/app/game-viewer/+models/game-fighter';

@Component({
  selector: 'app-match-log',
  templateUrl: './match-log.component.html',
  styleUrls: ['./match-log.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MatchLogComponent implements OnInit
{

  @Input()
  public roundTicks: RoundTick[];

  public print: Promise<RoundTick[]>;

  public fighters = new Map<string, GameFighter>();

  constructor(private rosterService: RosterService) { }

  ngOnInit()
  {
    this.print = this.getPrint();
  }

  private async getPrint()
  {
    const round0 = this.roundTicks.find(o => o.round === 0);

    for (const tick of round0.ticks)
    {
      const spawnTick = tick as FighterSpawnedTick;
      const fighter = new GameFighter();
      fighter.id = spawnTick.fighterId;
      fighter.health = spawnTick.health;
      fighter.currentHealth = fighter.health;
      fighter.energy = spawnTick.energy;
      fighter.currentEnergy = fighter.energy;
      fighter.positionX = spawnTick.positionX;
      fighter.positionY = spawnTick.positionY;

      const fightername = await this.rosterService.getFighterName(fighter.id).toPromise();
      if (fightername && fighter.name !== '')
      {
        fighter.name = fightername.name;
      }

      this.fighters.set(fighter.id, fighter);
    }

    return this.roundTicks.filter(round => round.round > 0);
  }

  private getName(id: string): string
  {
    const fighter = this.fighters.get(id);

    if (!fighter.name)
    {
      return fighter.id;
    }

    return fighter.name;
  }
}
