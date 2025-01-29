import { DbBracket } from './db-bracket';
import { DbTeam } from './db-team';

export class CreateTournament {
  name: string = '';
  startDate: string = '';
  endDate: string = '';
  type: string = '';
  description: string = '';
  teams: DbTeam[] = [];
  brackets: String[] = [];
}
