import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateTournament } from '../model/create-tournament';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  url = 'https://localhost:7163/api';
  constructor(private httpclient: HttpClient) {}

  getTeams(): Observable<any> {
    return this.httpclient.get(this.url + '/Tournament/getSelectionsTeams');
  }

  getCFTeams(): Observable<any> {
    return this.httpclient.get(this.url + '/Tournament/getLocalTeams');
  }

  setTournament(newTournament: CreateTournament): Observable<any> {
    console.log('VOY A ENVIAR');
    console.table(newTournament);
    console.log(this.url + '/Tournament');
    return this.httpclient.post(this.url + '/Tournament', newTournament);
  }

  /**
   * Allows all the needed CORS for requests
   * @returns Headers
   */
  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
      }),
    };
  };
}
