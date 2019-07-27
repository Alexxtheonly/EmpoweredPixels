import { Component, OnInit, Input, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { RoundTick } from '../../+models/round-tick';

@Component({
  selector: 'app-rounds-viewer',
  templateUrl: './rounds-viewer.component.html',
  styleUrls: ['./rounds-viewer.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoundsViewerComponent implements OnInit
{
  @ViewChild('consolePanel')
  consolePanel: ElementRef;

  public isReplayMatchResult: boolean;

  @Input()
  ticks: RoundTick[];

  public currentTick: number;

  public roundsConsole: RoundTick[] = new Array();

  constructor(private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit()
  {
  }

  public replayMatchResult(): void
  {
    this.currentTick = 1;
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
    this.roundsConsole = JSON.parse(JSON.stringify(this.ticks));
  }

}
