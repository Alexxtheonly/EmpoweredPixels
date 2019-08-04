import { BehaviorSubject, Subject } from 'rxjs';
import { BaseHubService } from './../../+services/base-hub.service';
import { Injectable } from '@angular/core';
import { Match } from '../+models/match';

@Injectable({
  providedIn: 'root'
})
export class MatchHubService extends BaseHubService
{
  public matchUpdated$ = new Subject<Match>();
  public matchCreated$ = new Subject<any>();

  constructor()
  {
    super('hub/match');
  }

  public joinGroup(match: Match): Promise<any>
  {
    if (this.IsConnected())
    {
      return this.hubConnection.invoke('JoinGroup', match.id);
    } else
    {
      this.connectionEstablished$.subscribe(result =>
      {
        if (result)
        {
          return this.hubConnection.invoke('JoinGroup', match.id);
        }
      });
    }
  }

  public leaveGroup(match: Match): Promise<any>
  {
    if (this.IsConnected)
    {
      return this.hubConnection.invoke('LeaveGroup', match.id);
    } else
    {
      this.connectionEstablished$.subscribe(result =>
      {
        if (result)
        {
          return this.hubConnection.invoke('LeaveGroup', match.id);
        }
      });
    }
  }

  protected register(): void
  {
    this.hubConnection.on('UpdateMatch', (data: Match) =>
    {
      this.matchUpdated$.next(data);
    });

    this.hubConnection.on('UpdateMatchBrowser', () =>
    {
      this.matchCreated$.next();
    });
  }

  protected unregister(): void
  {
    this.hubConnection.off('UpdateMatch');
    this.hubConnection.off('UpdateMatchBrowser');
  }
}
