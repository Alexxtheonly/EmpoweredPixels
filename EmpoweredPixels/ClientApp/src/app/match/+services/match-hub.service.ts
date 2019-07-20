import { BehaviorSubject, Subject } from 'rxjs';
import { BaseHubService } from './../../+services/base-hub.service';
import { Injectable } from '@angular/core';
import { Match } from '../+models/match';

@Injectable({
  providedIn: 'root'
})
export class MatchHubService extends BaseHubService {
  public matchUpdated$ = new Subject<Match>();

  constructor() {
    super('hub/match');
  }

  public joinGroup(match: Match): Promise<any> {
    return this.hubConnection.invoke('JoinGroup', match.id);
  }

  public leaveGroup(match: Match): Promise<any> {
    return this.hubConnection.invoke('LeaveGroup', match.id);
  }

  protected register(): void {
    this.hubConnection.on('UpdateMatch', (data: Match) => {
      this.matchUpdated$.next(data);
    });
  }

  protected unregister(): void {
    this.hubConnection.off('UpdateMatch');
  }
}
