import { DbBracket } from '../model/db-bracket';
import { DbTeam } from '../model/db-team';

export interface Tournaments {
  toId: string;
  name: string;
  startDate: string;
  endDate: string;
  type: string;
  description: string;
  teams: DbTeam[];
  brackets: DbBracket[];
}
