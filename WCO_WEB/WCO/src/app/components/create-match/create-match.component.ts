import { TournamentService } from '../../services/tournament.service';
import { MatchesService } from '../../services/matches.service';
import { Tournaments } from 'src/app/interface/tournaments';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, Input, OnInit } from '@angular/core';
import { Dropdown } from 'src/app/model/dropdown';
import { DbTeam } from 'src/app/model/db-team';
import { Match } from '../../model/match';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.css'],
})
export class CreateMatchComponent implements OnInit {
  tournamentsData: Tournaments[] = [];
  bracket: any;
  bracketID: any;
  datetime: string = '';
  newMatch: Match = new Match();
  allTournamentsTeams: DbTeam[] = [];
  myTeams: Dropdown[] = [];

  constructor(
    private matchservice: MatchesService,
    private service: TournamentService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.service
      .getTournamentbyID(localStorage.getItem('toID'))
      .subscribe((data) => (this.tournamentsData = data));

    this.bracket = localStorage.getItem('currentBracket');
    this.bracketID = localStorage.getItem('currentBracketID');

    this.service
      .getTournamentTeams(localStorage.getItem('toID'))
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

  /**
   * Metodo para mostrar alerta de error por 2 segundos
   * @param message1 Mensaje de error
   * @param message2 Mensaje para cerrar alerta
   */
  openError(message: string, message2: string) {
    this._snackBar.open(message, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',
    });
  }

  /**
   * Metodo para mostrar alerta de exito por 2 segundos
   * @param message1 Mensaje de exito
   * @param message2 Mensaje para cerrar alerta
   */
  openSuccess(message1: string, message2: string) {
    this._snackBar.open(message1, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'green-snackbar',
    });
  }
  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
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
    this.newMatch.bracketId = this.bracketID;
    const startTournamentDate = new Date(this.tournamentsData[0].startDate);
    const endTournamentDate = new Date(this.tournamentsData[0].endDate);
    const today = new Date();

    if (
      this.newMatch.venue == '' ||
      this.datetime == '' ||
      this.newMatch.idTeam1 == null ||
      this.newMatch.idTeam2 == null
    ) {
      this.openError(
        'Falta al menos uno de los espacios requeridos!',
        'Intente de nuevo'
      );
    } else if (today > startD) {
      this.openError(
        'La fecha ingresa es invalida!',
        'No se pueden crear partidos en el pasado'
      );
    } else if (startD < startTournamentDate || startD > endTournamentDate) {
      this.openError(
        'La fecha ingresa es invalida!',
        'No es posible crear el partido en fechas fuera de torneo'
      );
    } else if (this.newMatch.idTeam1 == this.newMatch.idTeam2) {
      this.openError(
        'Error al crear el partido!',
        'El equipo 1 y el equipo 2 son el mismo!'
      );
    } else {
      this.matchservice
        .addNewMatch(this.newMatch)
        .subscribe((match) => console.log(match));
      this.openSuccess('Partido creado con éxito', 'Ok');
      this.delay(50).then(() => {
        this.router.navigate(['/tournament-details']);
      });
    }
  }
}
