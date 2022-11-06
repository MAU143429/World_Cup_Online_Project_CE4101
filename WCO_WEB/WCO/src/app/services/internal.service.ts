import { EventEmitter, Injectable, Output } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { DbBracket } from '../model/db-bracket';

const initialBracket = {
  bId: 0,
  name: '',
  tounamentId: '',
};
@Injectable({
  providedIn: 'root',
})
export class InternalService {
  private selectedTournament = new BehaviorSubject<string>('');
  private selectedBracket = new BehaviorSubject<DbBracket>(initialBracket);

  get tournamentId(): Observable<string> {
    return this.selectedTournament.asObservable();
  }

  setTournamentId(newId: string): void {
    this.selectedTournament.next(newId);
  }

  get currentBracket(): Observable<DbBracket> {
    return this.selectedBracket.asObservable();
  }

  setCurrentBracket(newBracket: DbBracket): void {
    this.selectedBracket.next(newBracket);
  }
}
