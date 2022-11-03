import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Tournaments } from 'src/app/interface/tournaments';
import { DbTeam } from 'src/app/model/db-team';
import { Dropdown } from 'src/app/model/dropdown';
import { Match } from 'src/app/model/match';
import { InternalService } from 'src/app/services/internal.service';
import { MatchesService } from 'src/app/services/matches.service';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.css'],
})
export class CreateMatchComponent implements OnInit {
  tournamentsData: Tournaments[] = [];
  bracket: any;
  datetime: any;
  newMatch: Match = new Match();
  allTournamentsTeams: DbTeam[] = [];
  myTeams: Dropdown[] = [];
  //selectionsList:
  constructor(
    private game: MatchesService,
    private service: TournamentService,
    private connection: InternalService,
    private router: Router
  ) {}

  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  ngOnInit(): void {
    this.service
      .getTournamentbyID(this.connection.getTournamentKey())
      .subscribe((data) => (this.tournamentsData = data));

    this.bracket = this.connection.getSelectedBracket();
    this.service
      .getTournamentTeams(this.connection.getTournamentKey())
      .subscribe((data) => {
        this.allTournamentsTeams = data;

        var teams: Dropdown[] = [];
        this.allTournamentsTeams.forEach((value) => {
          var dropdownObject: Dropdown = { text: '', value: 0 };
          dropdownObject.text = value.name;
          dropdownObject.value = value.teId;
          teams.push(dropdownObject);
        });

        this.myTeams = teams;
      });
  }

  getDate(datetime: String) {
    return datetime.split('T', 1)[0];
  }

  getTime(datetime: String) {
    return datetime.split('T', 2)[1];
  }

  /**
   * Calls Matches Service and post a new match to the DB
   * @param newMatch model based on attributes identified for a match
   */
  insertMatch() {
    this.newMatch.date = this.getDate(this.datetime);
    this.newMatch.startTime = this.getTime(this.datetime);
    this.newMatch.bracketId = this.bracket.bId;
    console.table(this.newMatch);
    this.game
      .addNewMatch(this.newMatch)
      .subscribe((match) => console.log(this.newMatch));

    this.delay(100).then(() => {
      this.router.navigate(['/tournament-details']);
    });
  }
}
