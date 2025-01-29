import { Observable } from 'rxjs';
import { EnvUrlService } from './env-url.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class WcoService {
  constructor(private http: HttpClient, private envUrl: EnvUrlService) {}

  /**
   * Permite crear el la ruta completa para el envio de datos
   * @param route seccion 1 de ruta
   * @param envAddress seccion 2 de ruta
   * @returns la ruta completa
   */
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  };

  /**
   * Este metodo permite realizar todas las consultas GET de WCO
   * @param route Ruta a la que realiza la consulta
   * @returns Objeto observable con la respuesta del API
   */
  public getData(route: string): Observable<any> {
    return this.http.get(
      this.createCompleteRoute(route, this.envUrl.urlAddress)
    );
  }

  /**
   * Este metodo permite realizar todas las consultas POST de WCO
   * @param route Ruta a la que realiza la consulta
   * @param body Objeto que contiene la información a enviar
   * @returns Objeto observable con la respuesta del API
   */

  public create(route: string, body: any): Observable<any> {
    return this.http.post(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body,
      this.generateHeaders()
    );
  }

  /**
   * Este metodo permite realizar todas las consultas DELETE de WCO
   * @param route Ruta a la que realiza la consulta
   * @returns Objeto observable con la respuesta del API
   */

  public delete(route: string): Observable<any> {
    return this.http.delete(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      this.generateHeaders()
    );
  }

  /**
   * Este metodo permite realizar todas las consultas PUT de WCO
   * @param route Ruta a la que realiza la consulta
   * @param body JSON content for all data needed to POST an object
   * @returns Objeto observable con la respuesta del API
   */

  public edit(route: string, body: any): Observable<any> {
    return this.http.put(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body,
      this.generateHeaders()
    );
  }

  /**
   * Permite añadir los headers necesario para la consulta
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
