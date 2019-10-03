import { Component, OnInit, Input, ChangeDetectionStrategy } from '@angular/core';
import { RoundTick } from '../../+models/round-tick';
import { RosterService } from 'src/app/roster/+services/roster.service';
import { FighterSpawnedTick } from '../../+models/fighter-spawned-tick';
import { GameFighter } from 'src/app/game-viewer/+models/game-fighter';
import { MatchService } from 'src/app/match/+services/match.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-match-log',
  templateUrl: './match-log.component.html',
  styleUrls: ['./match-log.component.css'],
})
export class MatchLogComponent implements OnInit
{
  public roundTicks: RoundTick[];

  public print: RoundTick[];

  public fighters = new Map<string, GameFighter>();

  public fightersSelection: GameFighter[];

  public fighterId: string;

  constructor(
    private rosterService: RosterService,
    private matchService: MatchService,
    private route: ActivatedRoute)
  {
    route.queryParams.subscribe(result =>
    {
      this.fighterId = result['filter'];
    });
  }

  async ngOnInit()
  {
    const id = this.route.snapshot.paramMap.get('id');

    this.roundTicks = await this.matchService.getMatchRoundTicks(id).toPromise();
    this.print = await this.getPrint();
  }

  private async getPrint(): Promise<RoundTick[]>
  {
    const round0 = this.roundTicks.find(o => o.round === 0);

    for (const tick of round0.ticks)
    {
      const spawnTick = tick as FighterSpawnedTick;
      const fighter = new GameFighter();
      fighter.id = spawnTick.fighterId;
      fighter.health = spawnTick.health;
      fighter.currentHealth = fighter.health;
      fighter.positionX = spawnTick.positionX;
      fighter.positionY = spawnTick.positionY;

      const fightername = await this.rosterService.getFighterName(fighter.id).toPromise();
      if (fightername && fighter.name !== '')
      {
        fighter.name = fightername.name;
      }

      this.fighters.set(fighter.id, fighter);
    }

    this.fightersSelection = [...this.fighters.values()];

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
