import { FighterHealTick } from './../match-viewer/+models/fighter-heal-tick';
import { FighterConditionDamage } from './../match-viewer/+models/fighter-condition-damage';
import { FighterMovedByAttack } from './../match-viewer/+models/fighter-moved-by-attack';
import { UserFeedbackService } from './../+services/userfeedback.service';
import { FighterDiedTick } from './../match-viewer/+models/fighter-died-tick';
import { FighterAttackTick } from './../match-viewer/+models/fighter-attack-tick';
import { FighterMove } from './../match-viewer/+models/fighter-move';
import { RosterService } from './../roster/+services/roster.service';
import { FighterSpawnedTick } from './../match-viewer/+models/fighter-spawned-tick';
import { GameFighter } from './+models/game-fighter';
import { RoundTick } from './../match-viewer/+models/round-tick';
import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
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

  public round: number;

  public pause: boolean;

  public scale = 1;

  public interval = 1000;

  public playing: boolean;

  public followFighterId = '';

  constructor(
    private rosterService: RosterService,
    private changeDetectorRef: ChangeDetectorRef,
    private matchService: MatchService,
    private route: ActivatedRoute,
    private userfeedbackService: UserFeedbackService)
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
      }, error =>
      {
        this.userfeedbackService.error(error);
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
    this.round = undefined;

    const round0 = this.roundTicks.find(o => o.round === 0);
    round0.ticks.forEach(o =>
    {
      const spawnTick = o as FighterSpawnedTick;
      const fighter = this.fighters.get(spawnTick.fighterId);
      fighter.health = spawnTick.health;
      fighter.currentHealth = fighter.health;
      fighter.positionX = spawnTick.positionX;
      fighter.positionY = spawnTick.positionY;
      fighter.isDead = false;
    });
  }

  private async init()
  {
    const id: string = this.route.snapshot.paramMap.get('id');
    this.roundTicks = await this.matchService.getMatchRoundTicks(id).toPromise();
    this.prepareReplay();
  }

  public async play(): Promise<any>
  {
    this.playing = true;
    const rounds = this.roundTicks.filter(round => round.round > 0);

    for (const round of rounds)
    {
      this.round = round.round;
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
    if (tick.fighterId && tick.targetId && tick.nextX)
    {
      this.handleFighterMovedByAttack(tick as FighterMovedByAttack);
      return false;
    }

    if (tick.conditionId && tick.damage)
    {
      this.handleFighterConditionDamage(tick as FighterConditionDamage);
      return false;
    }

    if (tick.nextX)
    {
      this.handleFighterMove(tick as FighterMove);
      return true;
    }

    if (tick.damage && tick.skillId && tick.targetId)
    {
      await this.handleFighterAttack(tick as FighterAttackTick);
      return true;
    }

    if (tick.died)
    {
      this.handleFighterDied(tick as FighterDiedTick);
      return false;
    }

    if (tick.appliedHealing || tick.potentialHealing)
    {
      this.handleFighterHeal(tick as FighterHealTick);
    }

    if (tick.spawned)
    {
      this.handleFighterSpawn(tick as FighterSpawnedTick);
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
  }

  private handleFighterMovedByAttack(moved: FighterMovedByAttack)
  {
    const fighter = this.fighters.get(moved.targetId);
    fighter.positionX = moved.nextX;
    fighter.positionY = moved.nextY;
  }

  private handleFighterConditionDamage(conditionDamage: FighterConditionDamage)
  {
    const fighter = this.fighters.get(conditionDamage.fighterId);
    fighter.currentHealth -= conditionDamage.damage;
    if (fighter.currentHealth < 0)
    {
      fighter.currentHealth = 0;
    }
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

  private handleFighterSpawn(spawn: FighterSpawnedTick)
  {
    const fighter = this.fighters.get(spawn.fighterId);
    fighter.currentHealth = spawn.health;
    fighter.positionX = spawn.positionX;
    fighter.positionY = spawn.positionY;
    fighter.isDead = false;
    this.deadCount--;

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

  private handleFighterHeal(heal: FighterHealTick)
  {
    const fighter = this.fighters.get(heal.targetId);
    fighter.currentHealth += heal.appliedHealing;
  }
}
