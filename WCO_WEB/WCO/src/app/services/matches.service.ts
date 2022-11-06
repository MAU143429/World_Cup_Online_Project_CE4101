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
   * Permite enviar la informacion de un nuevo partido
   * @param newMatch objeto con la informacion del partido a crear.
   * @returns resultado del request de creacion
   */
  public addNewMatch(newMatch: Match): Observable<any> {
    return this.wco.create(this.url + '/AddMatch', newMatch);
  }

  /**
   * El metodo permite traer todos los partidos asociados a un torneo
   * @param id la llave del torneo
   * @returns Observable con la lista de fases y partidos asociados a ese torneo
   */
  public getMatchesByBracketId(id: any): Observable<any> {
    return this.wco.getData(this.url + '/getMatchesByTournamentId/' + id);
  }
}
