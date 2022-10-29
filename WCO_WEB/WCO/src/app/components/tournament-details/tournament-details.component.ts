import { Component, OnInit } from '@angular/core';
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
  tournamentsData: Tournaments[] = [];
  bracketsData: DbBracket[] = [];
  matchData: AllInfo[] = [];
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
    this.service
      .getTournamentbyID(this.connection.getTournamentKey())
      .subscribe((data) => (this.tournamentsData = data));

    this.delay(100).then(() => {
      this.bracketsData = this.tournamentsData[0].brackets;
    });

    this.delay(100).then(() => {
      this.matchService
        .getMatchesByBracketId(this.tournamentsData[0].toId)
        .subscribe((data) => {
          this.matchData = data;
          console.table(this.matchData);
        });
    });
  }

  toArray(answers: any) {
    return Object.keys(answers).map((key) => answers[key]);
  }

  redirectMatch(bracket: any) {
    this.connection.setSelectedBracket(bracket);
    this.delay(100).then(() => {
      this.router.navigate(['/create-match']);
    });
  }
  public openBracket(event: NgbPanelChangeEvent, bracket: any): void {
    console.log('Se abrio un bracket');
    var length = 0;
    this.matchData.forEach((value) => {
      length = length + 1;
    });
    console.log(length);

    /**
    for (let i = 0; i < this.matchData.length; i++) {
      console.log(this.matchData[i][0].bracketId);
      if (bracket.bId == this.matchData[i][0].bracketId) {
        this.bracketMatches = this.matchData[i];
        console.log(this.bracketMatches);
        return;
      }
    }*/
  }
}
