import { GameFighterDamageinfo } from './+models/game-fighter-damageinfo';
import { async } from '@angular/core/testing';
import { FighterRegeneratedEnergy } from './../match-viewer/+models/fighter-regenerated-energy';
import { FighterRegeneratedHealth } from './../match-viewer/+models/fighter-regenerated-health';
import { FighterDiedTick } from './../match-viewer/+models/fighter-died-tick';
import { FighterAttackTick } from './../match-viewer/+models/fighter-attack-tick';
import { FighterMove } from './../match-viewer/+models/fighter-move';
import { RosterService } from './../roster/+services/roster.service';
import { map } from 'rxjs/operators';
import { FighterSpawnedTick } from './../match-viewer/+models/fighter-spawned-tick';
import { GameFighter } from './+models/game-fighter';
import { RoundTick } from './../match-viewer/+models/round-tick';
import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { Tick } from '../match-viewer/+models/tick';
import { ActivatedRoute } from '@angular/router';
import { MatchService } from '../match/+services/match.service';

@Component({
  selector: 'app-game-viewer',
  templateUrl: './game-viewer.component.html',
  styleUrls: ['./game-viewer.component.css']
})
export class GameViewerComponent implements OnInit
{
  @Input()
  public roundTicks: RoundTick[];

  public fighters = new Map<string, GameFighter>();

  public deadCount = 0;

  public pause: boolean;

  public scale = 1;

  public interval = 1000;

  public playing: boolean;

  public followFighterId = '';

  constructor(
    private rosterService: RosterService,
    private changeDetectorRef: ChangeDetectorRef,
    private matchService: MatchService,
    private route: ActivatedRoute)
  {
  }

  async ngOnInit()
  {
    await this.init();
  }

  public scaleView(value: boolean): void
  {
    this.scale += value ? 0.1 : -0.1;
  }

  private prepareReplay()
  {
    const round0 = this.roundTicks.find(o => o.round === 0);
    round0.ticks.forEach(o =>
    {
      const spawnTick = o as FighterSpawnedTick;
      const fighter = new GameFighter();
      fighter.id = spawnTick.fighterId;
      fighter.health = spawnTick.health;
      fighter.currentHealth = fighter.health;
      fighter.energy = spawnTick.energy;
      fighter.currentEnergy = fighter.energy;
      fighter.positionX = spawnTick.positionX;
      fighter.positionY = spawnTick.positionY;
      this.rosterService.getFighterName(fighter.id).subscribe(result =>
      {
        if (!result)
        {
          fighter.name = fighter.id;
          return;
        }
        fighter.name = result.name;
      });
      this.fighters.set(fighter.id, fighter);
    });
  }

  public reset()
  {
    if (this.playing)
    {
      return;
    }

    this.deadCount = 0;

    const round0 = this.roundTicks.find(o => o.round === 0);
    round0.ticks.forEach(o =>
    {
      const spawnTick = o as FighterSpawnedTick;
      const fighter = this.fighters.get(spawnTick.fighterId);
      fighter.health = spawnTick.health;
      fighter.currentHealth = fighter.health;
      fighter.energy = spawnTick.energy;
      fighter.currentEnergy = fighter.energy;
      fighter.positionX = spawnTick.positionX;
      fighter.positionY = spawnTick.positionY;
      fighter.isDead = false;
    });
  }

  private async init()
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    const match = await this.matchService.getMatchResult(id).toPromise();
    this.roundTicks = match.ticks;
    this.prepareReplay();
  }

  public async play(): Promise<any>
  {
    this.playing = true;
    const rounds = this.roundTicks.filter(round => round.round > 0);

    for (const round of rounds)
    {
      for (const tick of round.ticks)
      {
        if (await this.handleTick(tick))
        {
          this.changeDetectorRef.markForCheck();
          await this.delay(this.getDelay());
        }
      }
    }

    this.playing = false;
  }

  private getDelay(): number
  {
    return this.interval / (this.fighters.size - this.deadCount);
  }

  /**
   * Returns true if a sleep is necesarry
   * @param tick
   */
  private async handleTick(tick: any): Promise<boolean>
  {
    if (tick.nextX)
    {
      this.handleFighterMove(tick as FighterMove);
      return true;
    }

    if (tick.targetId)
    {
      await this.handleFighterAttack(tick as FighterAttackTick);
      return true;
    }

    if (tick.died)
    {
      this.handleFighterDied(tick as FighterDiedTick);
      return false;
    }

    if (tick.healthPointsRegenerated)
    {
      this.handleFighterHealthReg(tick as FighterRegeneratedHealth);
      return false;
    }

    if (tick.regeneratedEnergy)
    {
      this.handleFighterEnergyReg(tick as FighterRegeneratedEnergy);
      return false;
    }
  }

  private delay(ms: number): Promise<any>
  {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  private async handleFighterAttack(attack: FighterAttackTick)
  {
    const attacker = this.fighters.get(attack.fighterId);
    const target = this.fighters.get(attack.targetId);

    target.currentHealth -= attack.damage;

    if (target.currentHealth < 0)
    {
      target.currentHealth = 0;
    }

    attacker.currentEnergy -= attack.energy;
  }

  private handleFighterDied(died: FighterDiedTick)
  {
    const fighter = this.fighters.get(died.fighterId);
    fighter.isDead = true;
    this.deadCount++;
  }

  private handleFighterMove(move: FighterMove)
  {
    const fighter = this.fighters.get(move.fighterId);
    fighter.positionX = move.nextX;
    fighter.positionY = move.nextY;

    if (fighter.id === this.followFighterId)
    {
      this.scrollTo(fighter.id);
    }
  }

  public scrollTo(id: string)
  {
    document.getElementById(id).scrollIntoView({
      behavior: 'auto',
      block: 'center',
      inline: 'center'
    });
  }

  private handleFighterHealthReg(healthReg: FighterRegeneratedHealth)
  {
    const fighter = this.fighters.get(healthReg.fighterId);
    fighter.currentHealth += healthReg.healthPointsRegenerated;
  }

  private handleFighterEnergyReg(energyReg: FighterRegeneratedEnergy)
  {
    const fighter = this.fighters.get(energyReg.fighterId);
    fighter.currentEnergy += energyReg.regeneratedEnergy;
  }
}
