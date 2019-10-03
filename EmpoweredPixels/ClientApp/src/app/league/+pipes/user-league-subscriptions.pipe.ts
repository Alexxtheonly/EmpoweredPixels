import { LeagueSubscription } from './../+models/league-subscription';
import { Pipe, PipeTransform } from '@angular/core';
import { LeagueService } from '../+services/league.service';
import { Observable } from 'rxjs';

@Pipe({
  name: 'userLeagueSubscriptions'
})
export class UserLeagueSubscriptionsPipe implements PipeTransform
{

  constructor(private leagueService: LeagueService)
  {
  }

  transform(leagueId: number): Observable<LeagueSubscription[]>
  {
    if (!leagueId)
    {
      return;
    }

    return this.leagueService.getUserSubscriptions(leagueId);
  }
}
