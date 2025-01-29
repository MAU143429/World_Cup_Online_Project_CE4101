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

  /**
   * El metodo permite traer un partido usando su id
   * @param id el id del partido
   * @returns Observable con la informacion de partido
   */
  public getMatchesById(id: any): Observable<any> {
    return this.wco.getData(this.url + '/GetMatchById/' + id);
  }

  /**
   * El metodo permite traer a todos los jugadores de un equipo
   * @param id del equipo
   * @returns Observable con la lista de jugadores
   */
  public getPlayersByTeamId(id: any): Observable<any> {
    return this.wco.getData(this.url + '/GetPlayersbyTeamId/' + id);
  }

  /**
   * El metodo permite traer a todos los jugadores de dos equipo
   * @param id1 del equipo 1
   * @param id2 del equipo 2
   * @returns Observable con la lista de jugadores
   */
  public getAllPlayersByTeamsId(id1: any, id2: any): Observable<any> {
    return this.wco.getData(
      this.url + '/GetPlayersbyBothTeamId/' + id1 + '/' + id2
    );
  }
}
