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

  /**
   * Permite traer los equipos que no esten ligados a un torneo
   * obteniendolos por el tipo de equipo (seleccion/local)
   * @param type el tipo de equipo que desea traer
   * @returns la lista de equipos disponible
   */
  getTeamsbyType(type: any): Observable<any> {
    return this.wco.getData(this.url + '/GetTeamsByType/' + type);
  }

  /**
   * Este metodo permite enviar la informacion para crear un torneo
   * @param newTournament objeto que contiene la inforamacion del nuevo torneo
   * @returns el resultado de guardar el torneo
   */
  setTournament(newTournament: CreateTournament): Observable<any> {
    console.table(newTournament);
    return this.wco.create(this.url + '/AddTournament', newTournament);
  }

  /**
   * Permite traer una lista de todos los torneos registrados en WCO
   * @returns las lista de todos los torneos
   */
  getTournaments(): Observable<any> {
    return this.wco.getData(this.url);
  }

  /**
   * Permite traer unicamente un torneo a traves de id de torneo (llave)
   * @param id la llave del torneo a consultar
   * @returns Observable con la informacion del torneo solicitado
   */
  getTournamentbyID(id: any): Observable<any> {
    return this.wco.getData(this.url + '/getTournamentById/' + id);
  }

  /**
   * Este metodo permite obtener la lista de equipos que este vinculado a un torneo
   * a traves de su TournamentId
   * @param id la llave del torneo a consultar
   * @returns Observable con la informacion de los equipos del torneo solicitado
   */
  getTournamentTeams(id: any): Observable<any> {
    return this.wco.getData(this.url + '/GetTeamsByTournamentId/' + id);
  }
}
