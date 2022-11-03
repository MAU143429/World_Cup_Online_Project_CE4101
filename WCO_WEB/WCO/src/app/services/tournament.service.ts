import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTournament } from '../model/create-tournament';
import { WcoService } from './wco.service';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  url = 'api/Tournament';

  constructor(private wco: WcoService) {}

  getTeamsbyType(type: any): Observable<any> {
    return this.wco.getData(this.url + '/GetTeamsByType/' + type);
  }

  setTournament(newTournament: CreateTournament): Observable<any> {
    console.table(newTournament);
    return this.wco.create(this.url + '/AddTournament', newTournament);
  }

  getTournaments(): Observable<any> {
    return this.wco.getData(this.url);
  }

  getTournamentbyID(id: any): Observable<any> {
    return this.wco.getData(this.url + '/getTournamentById/' + id);
  }

  getTournamentTeams(id: any): Observable<any> {
    return this.wco.getData(this.url + '/GetTeamsByTournamentId/' + id);
  }
}
