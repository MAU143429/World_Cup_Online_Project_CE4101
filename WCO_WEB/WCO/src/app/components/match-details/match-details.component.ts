import { Component, OnInit } from '@angular/core';
import { InternalService } from '../../services/internal.service';
import { CreatePrediction } from '../../model/create-prediction';
import { Prediction } from 'src/app/model/prediction';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DbMatch } from 'src/app/model/db-match';
import { MatchesService } from '../../../../src/app/services/matches.service';
import { DbPlayer } from 'src/app/model/db-player';
import { Dropdown } from 'src/app/model/dropdown';
import { Router } from '@angular/router';
import { PredictionsService } from '../../../../src/app/services/predictions.service';

var initialPrediction = {
  PId: 0,
  goals: 0,
  assists: 0,
};

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.css'],
})
export class MatchDetailsComponent implements OnInit {
  newPrediction: CreatePrediction = new CreatePrediction();
  myPrediction: CreatePrediction = new CreatePrediction();
  matchData: DbMatch[] = [];
  allPredictions: Prediction[] = [];
  goalsT1: number[] = new Array(21).fill(0);
  goalsT2: number[] = new Array(21).fill(0);
  assistsT1: number[] = new Array(20).fill(0);
  assistsT2: number[] = new Array(20).fill(0);
  currentMatch: number = 0;
  playersteam1: DbPlayer[] = [];
  playersteam2: DbPlayer[] = [];
  players: Dropdown[] = [];
  winOptions: any[] = [];

  constructor(
    private connection: InternalService,
    private matchService: MatchesService,
    private predictionService: PredictionsService,
    private _snackBar: MatSnackBar,
    private router: Router
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
    this.matchService
      .getMatchesById(localStorage.getItem('currentMatch'))
      .subscribe((data) => (this.matchData = data));
    this.delay(100).then(() => {
      var winT1: Dropdown = {
        text: this.matchData[0].teams[0].name,
        value: this.matchData[0].teams[0].teId,
      };
      var winT2: Dropdown = {
        text: this.matchData[0].teams[1].name,
        value: this.matchData[0].teams[1].teId,
      };
      var draw: Dropdown = { text: 'Empate', value: -1 };
      this.winOptions = [winT1, winT2, draw];
      this.matchService
        .getAllPlayersByTeamsId(
          this.matchData[0].teams[0].teId,
          this.matchData[0].teams[1].teId
        )
        .subscribe((data) => {
          var teams: Dropdown[] = [];
          data.forEach((value: any) => {
            var dropdownObject: Dropdown = { text: '', value: 0 };
            dropdownObject.text = value.name;
            dropdownObject.value = value.pId;
            teams.push(dropdownObject);
          });
          this.players = teams;
          var ownGoalT1: DbPlayer = { pId: 1000, name: 'Autogol', tId: 1000 };
          var ownGoalT2: DbPlayer = { pId: 1000, name: 'Autogol', tId: 1000 };
          this.playersteam1 = data.slice(0, 20);
          this.playersteam1.push(ownGoalT1);
          this.playersteam2 = data.slice(20, 40);
          this.playersteam2.push(ownGoalT2);
        });

      this.predictionService
        .getPredictionbyIds(
          localStorage.getItem('email'),
          localStorage.getItem('nickname'),
          this.matchData[0].mId
        )
        .subscribe((data) => {
          this.myPrediction = data;
          console.log(this.myPrediction);
        });
    });
  }

  /**
   * Este metodo permite asociar los partidos y asistencias a jugadores en concreto del equipo 1
   */
  associatePredictionsT1() {
    for (var i = 0; i < 20; i++) {
      if (this.goalsT1[i] != 0 || this.assistsT1[i] != 0) {
        var initialPrediction = {
          PId: 0,
          goals: 0,
          assists: 0,
        };
        initialPrediction.PId = this.playersteam1[i].pId;
        initialPrediction.goals = this.goalsT1[i];
        initialPrediction.assists = this.assistsT1[i];
        this.allPredictions.push(initialPrediction);
      }
    }
  }

  /**
   * Este metodo permite asociar los partidos y asistencias a jugadores en concreto del equipo 2
   */
  associatePredictionsT2() {
    for (var i = 0; i < 20; i++) {
      if (this.goalsT2[i] != 0 || this.assistsT2[i] != 0) {
        var initialPrediction2 = {
          PId: 0,
          goals: 0,
          assists: 0,
        };
        initialPrediction2.PId = this.playersteam2[i].pId;
        initialPrediction2.goals = this.goalsT2[i];
        initialPrediction2.assists = this.assistsT2[i];
        this.allPredictions.push(initialPrediction2);
      }
    }
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
   * Metodo que cuenta goles de equipo en prediccion
   * sumando los goles de cada
   * uno de sus jugadores.
   * @param teamGoals
   * @returns Team goals from scorers prediction.
   */
  goalCount(teamGoals: number[]) {
    let goals = 0;
    teamGoals.forEach((element) => {
      goals = goals + element;
    });
    console.log(goals);
    return goals;
  }

  /**
   * Metodo que cuenta goles de equipo en prediccion
   * sumando los goles de cada
   * uno de sus jugadores.
   * @param teamGoals
   * @returns Team goals from scorers prediction.
   */
  assistCount(teamAssists: number[]) {
    let assists = 0;
    teamAssists.forEach((element) => {
      assists = assists + element;
    });

    return assists;
  }

  /**
   * Este metodo permite guardar el resultado de una prediccion
   */
  addPrediction() {
    this.newPrediction.acc_email = localStorage.getItem('email');
    this.newPrediction.acc_nick = localStorage.getItem('nickname');
    this.newPrediction.TId = localStorage.getItem('toID');
    if (localStorage.getItem('scope') == 'admin') {
      this.newPrediction.isAdmin = true;
    }
    this.newPrediction.match_id = this.matchData[0].mId;
    this.associatePredictionsT1();
    this.delay(20).then(() => {
      this.associatePredictionsT2();

      console.log('Pid', this.newPrediction.PId);
      console.log('Winner', this.newPrediction.winner);
      // Verificar que campo de MVP se haya llenado
      if (this.newPrediction.winner == null) {
        this.openError(
          'Debe agregar un ganador para continuar',
          'Intente de nuevo'
        );
      } else if (this.newPrediction.PId == null) {
        this.openError(
          'Debe agregar un MVP para continuar',
          'Intente de nuevo'
        );
      } else if (
        !(
          this.goalCount(this.goalsT1) == this.newPrediction.goalsT1 &&
          this.goalCount(this.goalsT2) == this.newPrediction.goalsT2
        )
      ) {
        this.openError(
          'No hay coherencia entre goles de jugadores y marcador',
          'Ingrese una predicción coherente'
        );
      } else if (
        !(
          this.assistCount(this.assistsT1) <= this.newPrediction.goalsT1 &&
          this.assistCount(this.assistsT2) <= this.newPrediction.goalsT2
        )
      ) {
        this.openError(
          'No hay coherencia entre asistencia de jugadores y marcador',
          'Ingrese una predicción coherente'
        );
      } else {
        this.newPrediction.predictionPlayers = this.allPredictions;
        this.predictionService
          .addNewPrediction(this.newPrediction)
          .subscribe((data) => console.log(data));
        this.router.navigate(['/home']);
        console.log(this.newPrediction);
      }
    });
  }
}
