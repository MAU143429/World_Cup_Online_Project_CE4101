import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class InternalService {
  private selectedTournament: any;
  private selectedBracket: any;

  sendTournamentKey(tournamentKey: any) {
    this.selectedTournament = tournamentKey;
  }

  getTournamentKey(): Observable<any> {
    return this.selectedTournament;
  }

  setSelectedBracket(bracket: any) {
    this.selectedBracket = bracket;
  }
  getSelectedBracket(): Observable<any> {
    return this.selectedBracket;
  }
}
