import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
/**
 * @class Contains api url address for handling server requests from frontend
 */
export class EnvUrlService {
 public urlAddress: string = environment.apiUrl;
  constructor() { }
}
