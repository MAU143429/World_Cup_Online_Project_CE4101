import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class EnvUrlService {
  public urlAddress: string = environment.apiUrl;
  constructor() { }
}
