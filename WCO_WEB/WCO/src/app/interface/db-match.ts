import { DbTeam } from '../model/db-team';

export interface DbMatch {
  mId: number;
  startTime: string;
  date: string;
  venue: string;
  bracketId: number;
  teams: DbTeam[];
}
