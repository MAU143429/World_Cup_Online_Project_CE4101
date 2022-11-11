import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreatePrediction } from '../model/create-prediction';
import { WcoService } from './wco.service';

@Injectable({
  providedIn: 'root',
})
export class PredictionsService {
  url = 'api/Prediction';
  constructor(private wco: WcoService) {}

  /**
   * Permite enviar la informacion de una prediccion
   * @param newPrediction objeto con la informacion de la prediccion
   * @returns resultado del request de creacion
   */
  public addNewPrediction(newPrediction: CreatePrediction): Observable<any> {
    return this.wco.create(this.url + '/AddPrediction', newPrediction);
  }

  /**
   * Permite traer la informacion de una prediccion
   * @returns resultado del request de creacion
   */
  public getPredictionbyIds(
    email: any,
    nickname: any,
    id_match: any
  ): Observable<any> {
    return this.wco.getData(
      this.url +
        '/getPredictionByNEM/' +
        nickname +
        '/' +
        email +
        '/' +
        id_match
    );
  }
}
