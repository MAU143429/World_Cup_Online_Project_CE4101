import { TournamentService } from '../../services/tournament.service';
import { MatchesService } from '../../services/matches.service';
import { Tournaments } from '../../interface/tournaments';
import { DbBracket } from 'src/app/model/db-bracket';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css'],
})
export class TournamentDetailsComponent implements OnInit {
  scope: any;
  tournamentsData: Tournaments[] = [];
  bracketsData: DbBracket[] = [];
  matchData: any[] = [];

  constructor(
    private service: TournamentService,
    private matchService: MatchesService
  ) {}

  ngOnInit(): void {
    this.scope = localStorage.getItem('scope');
    this.service
      .getTournamentbyID(localStorage.getItem('toID'))
      .subscribe((data) => {
        this.tournamentsData = data;
        this.bracketsData = this.tournamentsData[0].brackets;
      });
    this.matchService
      .getMatchesByBracketId(localStorage.getItem('toID'))
      .subscribe((data) => (this.matchData = data));
  }

  /**
   * Este metodo permite enviar la informacion sobre el bracket en donde se encuentra
   * el partido almacenado que se desea abrir.
   * @param bracket bracket donde esta el partido
   */
  redirectMatch(bracket: any) {
    localStorage.setItem('currentBracket', bracket.name);
  }

  /**
   * Este metodo permite enviar la informacion sobre el partido que se desea abrir.
   * @param match numero del partido que se desea abrir
   */
  viewMatch(match: any) {
    localStorage.setItem('currentMatch', match);
  }
}
