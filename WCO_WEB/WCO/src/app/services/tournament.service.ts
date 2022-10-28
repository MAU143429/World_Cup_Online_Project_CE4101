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
    return this.httpclient.get(
      this.url + '/Tournament/GetTeamsByType/Selection'
    );
  }

  getCFTeams(): Observable<any> {
    return this.httpclient.get(this.url + '/Tournament/GetTeamsByType/Local');
  }

  setTournament(newTournament: CreateTournament): Observable<any> {
    console.table(newTournament);
    return this.httpclient.post(
      this.url + '/Tournament/AddTournament',
      newTournament
    );
  }

  getTournaments(): Observable<any> {
    return this.httpclient.get(this.url + '/Tournament');
  }

  getTournamentbyID(id: any): Observable<any> {
    return this.httpclient.get(this.url + '/Tournament/' + id);
  }
}
