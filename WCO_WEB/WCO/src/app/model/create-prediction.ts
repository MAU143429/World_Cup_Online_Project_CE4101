import { Prediction } from './prediction';

export class CreatePrediction {
  goalsT1: number = 0;
  goalsT2: number = 0;
  PId: number = 0;
  predictionPlayers: Array<Prediction> = [];
  acc_nick: any;
  acc_email: any;
  match_id: number = 0;
}
