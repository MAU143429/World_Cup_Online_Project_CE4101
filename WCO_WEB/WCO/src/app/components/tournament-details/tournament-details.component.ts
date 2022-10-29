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
@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css'],
})
export class TournamentDetailsComponent implements OnInit {
  tournamentsData: Tournaments[] = [];
  bracketsData: DbBracket[] = [];
  matchData: DbMatch[] = [
    {
      mId: 0,
      startTime: '',
      date: '',
      venue: '',
      bracketId: 0,
      teams: [
        {
          teId: 0,
          name: '',
        },
        {
          teId: 0,
          name: '',
        },
      ],
    },
  ];

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
  }

  redirectMatch(bracket: any) {
    this.connection.setSelectedBracket(bracket);
    this.delay(100).then(() => {
      this.router.navigate(['/create-match']);
    });
  }
  public openBracket(event: NgbPanelChangeEvent, bracket: any): void {
    if (event.nextState === true) {
      this.matchService.getMatchesByBracketId(bracket.bId).subscribe((data) => {
        this.matchData = data;
        console.log(this.matchData);
      });
    }
  }
}
