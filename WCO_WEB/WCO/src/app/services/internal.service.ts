import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class InternalService {
  private selectedTournament: any;

  sendTournamentKey(tournamentKey: any) {
    this.selectedTournament = tournamentKey;
  }

  getTournamentKey(): Observable<any> {
    return this.selectedTournament;
  }
}
