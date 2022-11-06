import { Component, Input, OnInit } from '@angular/core';
import { TournamentService } from '../../services/tournament.service';
import { Tournaments } from '../../interface/tournaments';
import { InternalService } from '../../services/internal.service';
import { BracketName } from '../../interface/bracket-name';
import { DbBracket } from 'src/app/model/db-bracket';
import { Router } from '@angular/router';
import {
  NgbPanelChangeEvent,
  NgbPanelToggle,
} from '@ng-bootstrap/ng-bootstrap';
import { MatchesService } from 'src/app/services/matches.service';
import { DbMatch } from 'src/app/interface/db-match';
import { AllInfo } from 'src/app/interface/all-info';

@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css'],
})
export class TournamentDetailsComponent implements OnInit {
  tournamentID: string = '';
  tournamentsData: Tournaments[] = [];
  bracketsData: DbBracket[] = [];
  matchData: any[] = [];
  bracketMatches: DbMatch[] = [];

  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  constructor(
    private service: TournamentService,
    private matchService: MatchesService,
    private connection: InternalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.connection.tournamentId.subscribe(
      (data) => (this.tournamentID = data)
    );

    this.delay(50).then(() => {
      this.service
        .getTournamentbyID(this.tournamentID)
        .subscribe((data) => (this.tournamentsData = data));
    });

    this.delay(100).then(() => {
      this.bracketsData = this.tournamentsData[0].brackets;
      this.matchService
        .getMatchesByBracketId(this.tournamentsData[0].toId)
        .subscribe((data) => (this.matchData = data));
    });
  }

  toArray(answers: any) {
    return Object.keys(answers).map((key) => answers[key]);
  }

  redirectMatch(bracket: any) {
    console.log('SOY BRACKET', bracket);
    this.connection.setCurrentBracket(bracket);
    this.delay(100).then(() => {
      this.router.navigate(['/create-match']);
    });
  }
}
