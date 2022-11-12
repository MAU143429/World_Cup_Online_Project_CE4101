import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Tournaments } from 'src/app/interface/tournaments';
import { DbTeam } from 'src/app/model/db-team';
import { Dropdown } from 'src/app/model/dropdown';
import { Match } from '../../model/match';
import { InternalService } from '../../services/internal.service';
import { MatchesService } from '../../services/matches.service';
import { TournamentService } from '../../services/tournament.service';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.css'],
})
export class CreateMatchComponent implements OnInit {
  tournamentID = '';
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

  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  ngOnInit(): void {
    this.connection.tournamentId.subscribe(
      (data) => (this.tournamentID = data)
    );
    this.service
      .getTournamentbyID(this.tournamentID)
      .subscribe((data) => (this.tournamentsData = data));

    this.connection.currentBracket.subscribe((data) => (this.bracket = data));
    this.service.getTournamentTeams(this.tournamentID).subscribe((data) => {
      this.allTournamentsTeams = data;

      var teams: Dropdown[] = [];
      this.allTournamentsTeams.forEach((value) => {
        var dropdownObject: Dropdown = { text: '', value: 0 };
        dropdownObject.text = value.name;
        dropdownObject.value = value.teId;
        teams.push(dropdownObject);
      });
      console.log(teams);
      this.myTeams = teams;
    });
  }

  /**
   * Permite transformar un datetime en solo la fecha
   * @param datetime la fecha en formato datetime
   * @returns un string con la fecha
   */
  getDate(datetime: String) {
    return datetime.split('T', 1)[0];
  }
  /**
   * Permite transformar un datetime en solo la hora
   * @param datetime la hora en formato datetime
   * @returns un string con la hora
   */
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
   * Este metodo realiza todas las verificaciones para poder insertar un partido
   * y posteriormente realizar el envio de estos datos al API
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
