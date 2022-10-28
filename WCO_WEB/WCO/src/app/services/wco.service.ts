import { EnvUrlService } from './env-url.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class WcoService {
  constructor(private http: HttpClient, private envUrl: EnvUrlService) {}

  /**
   * Creates full path for a relative object address in api
   * @param route
   * @param envAddress
   * @returns
   */
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  };

  /**
   * GET request
   * @param route for relative endpoint
   * @returns Observable with server response
   */
  public getData = (route: string) => {
    return this.http.get(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      { observe: 'response' }
    );
  };

  /**
   * POST request
   * @param route for relative endpoint
   * @param body JSON content for all data needed to POST an object
   * @returns Observable with server response
   */
  public create = (route: string, body: any) => {
    return this.http.post(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body,
      { headers: this.generateHeaders().headers, observe: 'response' }
    );
  };

  /**
   * DELETE request
   * @param route for relative endpoint
   * @returns Observable with server response
   */
  public delete = (route: string) => {
    return this.http.delete(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      { headers: this.generateHeaders().headers, observe: 'response' }
    );
  };

  /**
   * PUT request
   * @param route for relative endpoint
   * @param body JSON content for all data needed to POST an object
   * @returns Observable with server response
   */
  public edit = (route: string, body: any) => {
    return this.http.put(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body,
      { headers: this.generateHeaders().headers, observe: 'response' }
    );
  };

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
