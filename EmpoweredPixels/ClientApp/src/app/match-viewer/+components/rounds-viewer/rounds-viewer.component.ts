import { Component, OnInit, Input, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { RoundTick } from '../../+models/round-tick';
import { FighterMove } from '../../+models/fighter-move';

@Component({
  selector: 'app-rounds-viewer',
  templateUrl: './rounds-viewer.component.html',
  styleUrls: ['./rounds-viewer.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoundsViewerComponent implements OnInit
{
  @ViewChild('consolePanel', { static: false })
  consolePanel: ElementRef;

  public isReplayMatchResult: boolean;

  @Input()
  set matchScores(scores: any[])
  {
    scores.forEach(o =>
    {
      const fighter = new FighterMove();
      fighter.fighterId = o.Id;
      this.fighters.push(fighter);
    });
  }

  @Input()
  ticks: RoundTick[];

  public currentTick: number;

  public roundsConsole: RoundTick[] = new Array();
  public fighters: FighterMove[] = new Array();
  public zoom = 1;
  constructor(
    private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit()
  {
  }

  public zoomIn()
  {
    this.zoom++;
  }

  public zoomOut()
  {
    this.zoom--;
  }

  public play()
  {
    this.currentTick = 0;
    this.roundsConsole = [];
    this.roundsConsole.push(this.ticks[this.currentTick]);

    const looper = setInterval(() =>
    {
      this.forward();

      if (this.currentTick === this.ticks.length - 1)
      {
        clearInterval(looper);
      }
    }, 1000);
  }

  public replayMatchResult(): void
  {
    this.currentTick = 0;
    this.roundsConsole = [];
    this.roundsConsole.push(this.ticks[this.currentTick]);
    this.isReplayMatchResult = true;
    this.changeDetectorRef.markForCheck();
  }

  public backward(): void
  {
    this.currentTick--;
    this.roundsConsole.pop();
    this.changeDetectorRef.markForCheck();
  }

  public forward(): void
  {
    this.currentTick++;
    this.roundsConsole.push(this.ticks[this.currentTick]);
    this.changeDetectorRef.markForCheck();

    setTimeout(() =>
    {
      const element: HTMLElement = this.consolePanel.nativeElement;
      element.scrollTop = element.scrollHeight;
    }, 1);
  }

  public showAllRounds(): void
  {
    this.roundsConsole = this.ticks;
  }

  public setPositionOfFighter(tick: any): void
  {
    const fighter = this.fighters.find(o => o.fighterId == tick.FighterId);

    if (fighter == null)
    {
      return;
    }

    fighter.currentX = tick.CurrentX;
    fighter.currentY = tick.CurrentY;
  }

}
