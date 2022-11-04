import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateAccount } from '../model/create-account';
import { WcoService } from './wco.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  url = 'api/Account';

  constructor(private wco: WcoService) {}

  /**
   * Este metodo nos permite comunicarnos con API para enviar
   * toda la información necesaria para crear una cuenta
   * @param newTournament objeto con la información de la cuenta a crear
   * @returns status de la creación de la cuenta.
   */
  createAccount(newAccount: CreateAccount): Observable<any> {
    console.table(newAccount);
    return this.wco.create(this.url + '/AddAccount', newAccount);
  }

  /**
   * Este metodo nos permite traer la información de una cuenta vinculada a su email.
   * @param email email con el que se realizara la búsqueda
   * @returns cuenta de usuario en caso de existir, lista vacia si no existe cuenta.
   */
  getAccountByEmail(email: any): Observable<any> {
    return this.wco.getData(this.url + '/GetAccountByEmail/' + email);
  }

  /**
   * Este metodo nos permite traer la información de una cuenta vinculada a un nickname.
   * @param nickname nickname con el que se realizara la búsqueda
   * @returns cuenta de usuario en caso de existir, lista vacia si no existe cuenta.
   */
  getAccountByNickname(nickname: any): Observable<any> {
    return this.wco.getData(this.url + '/GetAccountByNickname/' + nickname);
  }
}
