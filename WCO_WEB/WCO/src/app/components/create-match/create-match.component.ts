import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
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
  datetime: string = '';
  newMatch: Match = new Match();
  allTournamentsTeams: DbTeam[] = [];
  myTeams: Dropdown[] = [];

  constructor(
    private game: MatchesService,
    private service: TournamentService,
    private connection: InternalService,
    private router: Router,
    private _snackBar: MatSnackBar
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

  openSnackBar(message: string, message2: string) {
    this._snackBar.open(message, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',
    });
  }

  /**
   * Calls Matches Service and post a new match to the DB
   * @param newMatch model based on attributes identified for a match
   */
  insertMatch() {
    console.log(this.newMatch.idTeam1);
    console.log(this.newMatch.idTeam2);
    var startD = new Date();
    if (this.datetime != '') {
      this.newMatch.date = this.getDate(this.datetime);
      this.newMatch.startTime = this.getTime(this.datetime);
      startD = new Date(this.newMatch.date);
    }
    this.newMatch.bracketId = this.bracket.bId;
    const startTournamentDate = new Date(this.tournamentsData[0].startDate);
    const endTournamentDate = new Date(this.tournamentsData[0].endDate);
    const today = new Date();

    if (
      this.newMatch.venue == '' ||
      this.datetime == '' ||
      this.newMatch.idTeam1 == null ||
      this.newMatch.idTeam2 == null
    ) {
      this.openSnackBar(
        'Falta al menos uno de los espacios requeridos!',
        'Intente de nuevo'
      );
    } else if (today > startD) {
      this.openSnackBar(
        'La fecha ingresa es invalida!',
        'No se pueden crear partidos en el pasado'
      );
    } else if (startD < startTournamentDate || startD > endTournamentDate) {
      this.openSnackBar(
        'La fecha ingresa es invalida!',
        'No es posible crear el partido en fechas fuera de torneo'
      );
    } else if (this.newMatch.idTeam1 == this.newMatch.idTeam2) {
      this.openSnackBar(
        'Error al crear el partido!',
        'El equipo 1 y el equipo 2 son el mismo!'
      );
    } else {
      this.game
        .addNewMatch(this.newMatch)
        .subscribe((match) => console.log(match));

      this.delay(100).then(() => {
        this.router.navigate(['/tournament-details']);
      });
    }
  }
}
