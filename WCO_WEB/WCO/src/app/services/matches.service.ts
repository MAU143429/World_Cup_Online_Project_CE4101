import { Match } from './../model/match';
import { WcoService } from './wco.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MatchesService {
  url = 'api/Match';
  constructor(private wco: WcoService) {}

  /**
   * Adds a new match for a given tournament and posts it to the database
   * @param newMatch object extracted from users response to the form
   * @returns
   */
  public addNewMatch(newMatch: Match): Observable<any> {
    return this.wco.create(this.url + '/AddMatch', newMatch);
  }

  public getMatchesByBracketId(id: any): Observable<any> {
    return this.wco.getData(this.url + '/getMatchesByTournamentId/' + id);
  }
}
