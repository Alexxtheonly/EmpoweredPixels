import { Pipe, PipeTransform } from '@angular/core';
import { LeagueService } from '../+services/league.service';
import { Observable } from 'rxjs';
import { LeagueMatchWinner } from '../+models/league-match-winner';

@Pipe({
  name: 'lastLeagueWinner'
})
export class LastLeagueWinnerPipe implements PipeTransform
{

  constructor(private leagueService: LeagueService)
  {
  }

  transform(leagueId: number): Observable<LeagueMatchWinner>
  {
    if (!leagueId)
    {
      return;
    }

    return this.leagueService.getLastLeagueWinner(leagueId);
  }

}
