import { DbTeam } from './db-team';

export class DbMatch {
  mId: number = 0;
  startTime: string = '';
  date: string = '';
  venue: string = '';
  scoreT1: number = 0;
  scoreT2: number = 0;
  bracketId: string = '';
  teams: Array<DbTeam> = [];
}
