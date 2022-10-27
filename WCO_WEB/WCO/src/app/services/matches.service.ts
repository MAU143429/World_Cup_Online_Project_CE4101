import { Match } from './../model/match';
import { WcoService } from './wco.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatchesService {
  url = "api/Match"
  constructor(private wco:WcoService) { }

  public addNewMatch(newMatch:Match):Observable<any>{
    return this.wco.create(this.url, newMatch)
  }
  //public getTournaments():
}
