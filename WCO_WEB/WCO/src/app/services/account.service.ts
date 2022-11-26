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
   * @param newAccount objeto con la información de la cuenta a crear
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
    return this.wco.getData(
      this.url + '/GetInformationAccountByEmail/' + email
    );
  }

  /**
   * Este metodo nos permite conocer si un miembro se puede unir a un grupo en especifico o crear uno
   * @param email email del usuario
   * @param nickname nickname del usuario
   * @param toID id de torneo
   * @returns bool con el estado de la consulta
   */
  getIsInGroup(email: any, nickname: any, toID: any): Observable<any> {
    return this.wco.getData(
      this.url + '/isAccountInGroup/' + toID + '/' + nickname + '/' + email
    );
  }

  /**
   * Este metodo nos permite traer la información de una cuenta vinculada a un nickname.
   * @param nickname nickname con el que se realizara la búsqueda
   * @returns cuenta de usuario en caso de existir, lista vacia si no existe cuenta.
   */
  getAccountByNickname(nickname: any): Observable<any> {
    return this.wco.getData(this.url + '/GetAccountByNickname/' + nickname);
  }

  /**
   * Este metodo nos permite traer la información de todos los grupos a los que pertenece un usuario
   * @param nickname nickname del usuario actual
   * @param email email del usuario actual
   * @returns todos los grupos del usuario actual
   */
  getUserGroups(nickname: any, email: any): Observable<any> {
    return this.wco.getData(
      this.url + '/GetGroupsByNE/' + nickname + '/' + email
    );
  }

  /**
   * Este metodo nos permite traer la información de un grupo utilizando el id de este
   * @param gId el id del grupo
   * @returns la información del grupo
   */
  getGroupByID(gId: any): Observable<any> {
    return this.wco.getData(this.url + '/GetGroupById/' + gId);
  }

  /**
   * Este metodo nos permite traer la información relacionada al puntaje de torneo
   * @param toID id del torneo
   * @returns todos los puntajes en orden ascendente del torneo especificado
   */
  getTournamentScore(toID: any): Observable<any> {
    return this.wco.getData(this.url + '/GetScoreByTournamentId/' + toID);
  }

  /**
   * Este metodo nos permite traer la información relacionada a un grupo
   * @param gID id del grupo
   * @returns todos la informacion del grupo especificado
   */
  getGroupInfo(gId: any): Observable<any> {
    return this.wco.getData(this.url + '/GetGroupById/' + gId);
  }

  /**
   * Este metodo nos permite traer la información relacionada al puntaje de un grupo
   * @param gID id del grupo
   * @returns todos los puntajes en orden ascendente del grupo especificado
   */
  getGroupScore(gID: any): Observable<any> {
    return this.wco.getData(this.url + '/GetScoreByGroupId/' + gID);
  }

  /**
   * Este metodo nos permite crear un grupo privado
   * @param newGroup informacion del grupo a crear
   * @returns el resultado del grupo creado
   */
  newGroup(newGroup: any): Observable<any> {
    return this.wco.create(this.url + '/AddGroup', newGroup);
  }

  /**
   * Este metodo nos permite que un usuario se una a un grupo
   * @param newUser informacion del usuario y del grupo
   * @returns el resultado de la union
   */
  joinGroup(newUser: any): Observable<any> {
    return this.wco.create(this.url + '/AddAccountToGroup', newUser);
  }

  /**
   * Este metodo nos permite verificar las credenciales del usuario.
   * @param login datos de inicio de sesion
   * @returns boolean que nos indica si el login puede proceder
   */
  newLogin(login: any): Observable<any> {
    return this.wco.create(this.url + '/Login', login);
  }

  /**
   * Este metodo nos permite conocer el rol que tiene el usuario logeado
   * @returns el rol que tiene el usuario logeado
   */
  getRole(): Observable<any> {
    return this.wco.getData(
      this.url + '/Role/' + localStorage.getItem('email')
    );
  }
}
