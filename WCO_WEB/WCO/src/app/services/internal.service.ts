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

  /**
   * Este metodo permite a traves de un behavior subject acceder a la informacion
   * almacenada en una variable de interes (id de torneo) y la devuelve como observable
   */
  get tournamentId(): Observable<string> {
    return this.selectedTournament.asObservable();
  }

  /**
   * Este metodo permite modificar el behavior subject cambiando su valor.
   * @param newId el id de torneo nuevo
   */
  setTournamentId(newId: string): void {
    this.selectedTournament.next(newId);
  }

  /**
   * Este metodo permite a traves de un behavior subject acceder a la informacion
   * almacenada en una variable de interes (breacket de torneo) y la devuelve como observable
   */
  get currentBracket(): Observable<DbBracket> {
    return this.selectedBracket.asObservable();
  }

  /**
   * Este metodo permite modificar el behavior subject cambiando su valor.
   * @param newBracket el bracket actual
   */
  setCurrentBracket(newBracket: DbBracket): void {
    this.selectedBracket.next(newBracket);
  }
}
