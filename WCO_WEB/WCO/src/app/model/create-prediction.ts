import { Prediction } from './prediction';

export class CreatePrediction {
  goalsT1: number = 0;
  goalsT2: number = 0;
  winner: number = -1;
  TId: any;
  PId: number = -1;
  predictionPlayers: Array<Prediction> = [];
  acc_nick: any;
  acc_email: any;
  match_id: number = 0;
  isAdmin: boolean = false;
}
