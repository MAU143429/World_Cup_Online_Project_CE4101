import { Prediction } from './prediction';

export class CreatePrediction {
  goalsT1: number = 0;
  goalsT2: number = 0;
  PId: number = 0;
  predictionPlayer: Array<Prediction> = [];
  acc_nick: string = '';
  acc_email: string = '';
  match_id: number = 0;
}
