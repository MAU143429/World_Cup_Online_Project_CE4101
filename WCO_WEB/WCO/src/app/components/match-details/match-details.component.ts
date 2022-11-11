import { Component, OnInit } from '@angular/core';
import { InternalService } from '../../services/internal.service';
import { CreatePrediction } from '../../model/create-prediction';
import { Prediction } from 'src/app/model/prediction';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  allPreditions: Prediction[] = [];
  goalsT1: number[] = new Array(22).fill(0);
  goalsT2: number[] = new Array(22).fill(0);
  assistsT1: number[] = new Array(22).fill(0);
  assistsT2: number[] = new Array(22).fill(0);
  matchData = [
    {
      mId: '1',
      startTime: '14:00',
      date: '12-12-2022',
      venue: 'Estadio Nacional de Costa Rica',
      bracketId: '1',
      teams: [
        {
          teId: '1',
          name: 'Argentina',
        },
        {
          teId: '2',
          name: 'Costa Rica',
        },
      ],
    },
  ];

  playersteam1 = [
    {
      id: 1,
      name: 'Jugador 1.1',
    },
    {
      id: 2,
      name: 'Jugador 1.2',
    },
    {
      id: 3,
      name: 'Jugador 1.3',
    },
    {
      id: 4,
      name: 'Jugador 1.4',
    },
    {
      id: 5,
      name: 'Jugador 1.5',
    },
    {
      id: 6,
      name: 'Jugador 1.6',
    },
    {
      id: 7,
      name: 'Jugador 1.7',
    },
    {
      id: 8,
      name: 'Jugador 1.8',
    },
    {
      id: 9,
      name: 'Jugador 1.9',
    },
    {
      id: 10,
      name: 'Jugador 1.10',
    },
    {
      id: 11,
      name: 'Jugador 1.11',
    },
    {
      id: 12,
      name: 'Jugador 1.12',
    },
    {
      id: 13,
      name: 'Jugador 1.13',
    },
    {
      id: 14,
      name: 'Jugador 1.14',
    },
    {
      id: 15,
      name: 'Jugador 1.15',
    },
    {
      id: 16,
      name: 'Jugador 1.16',
    },
    {
      id: 17,
      name: 'Jugador 1.17',
    },
    {
      id: 18,
      name: 'Jugador 1.18',
    },
    {
      id: 19,
      name: 'Jugador 1.19',
    },
    {
      id: 20,
      name: 'Jugador 1.20',
    },
    {
      id: 21,
      name: 'Jugador 1.21',
    },
    {
      id: 22,
      name: 'Jugador 1.22',
    },
  ];
  playersteam2 = [
    {
      id: 23,
      name: 'Jugador 2.1',
    },
    {
      id: 24,
      name: 'Jugador 2.2',
    },
    {
      id: 25,
      name: 'Jugador 2.3',
    },
    {
      id: 26,
      name: 'Jugador 2.4',
    },
    {
      id: 27,
      name: 'Jugador 2.5',
    },
    {
      id: 28,
      name: 'Jugador 2.6',
    },
    {
      id: 29,
      name: 'Jugador 2.7',
    },
    {
      id: 30,
      name: 'Jugador 2.8',
    },
    {
      id: 31,
      name: 'Jugador 2.9',
    },
    {
      id: 32,
      name: 'Jugador 2.10',
    },
    {
      id: 33,
      name: 'Jugador 2.11',
    },
    {
      id: 34,
      name: 'Jugador 2.12',
    },
    {
      id: 35,
      name: 'Jugador 2.13',
    },
    {
      id: 36,
      name: 'Jugador 2.14',
    },
    {
      id: 37,
      name: 'Jugador 2.15',
    },
    {
      id: 38,
      name: 'Jugador 2.16',
    },
    {
      id: 39,
      name: 'Jugador 2.17',
    },
    {
      id: 40,
      name: 'Jugador 2.18',
    },
    {
      id: 41,
      name: 'Jugador 2.19',
    },
    {
      id: 42,
      name: 'Jugador 2.20',
    },
    {
      id: 43,
      name: 'Jugador 2.21',
    },
    {
      id: 44,
      name: 'Jugador 2.22',
    },
  ];

  players = ['Neymar Jr', 'Cristiano Ronaldo', 'Lionel Messi'];

  constructor(
    private connection: InternalService,
    private _snackBar: MatSnackBar) {}

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
    this.connection.currentMatch.subscribe((data) => console.log(data));
  }

  associatePredictionsT1() {
    for (var i = 0; i < 22; i++) {
      if (this.goalsT1[i] != 0 || this.assistsT1[i] != 0) {
        var initialPrediction = {
          PId: 0,
          goals: 0,
          assists: 0,
        };
        initialPrediction.PId = this.playersteam1[i].id;
        initialPrediction.goals = this.goalsT1[i];
        initialPrediction.assists = this.assistsT1[i];
        this.allPreditions.push(initialPrediction);
      }
    }
  }

  associatePredictionsT2() {
    for (var i = 0; i < 22; i++) {
      if (this.goalsT2[i] != 0 || this.assistsT2[i] != 0) {
        var initialPrediction2 = {
          PId: 0,
          goals: 0,
          assists: 0,
        };
        initialPrediction2.PId = this.playersteam2[i].id;
        initialPrediction2.goals = this.goalsT2[i];
        initialPrediction2.assists = this.assistsT2[i];
        this.allPreditions.push(initialPrediction2);
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
    teamGoals.forEach( (element) => {
      goals = goals + element
    })
    console.log(goals)
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
      teamAssists.forEach( (element) => {
        assists = assists + element
      })
  
      return assists;
    }

  addPrediction() {
    
    this.associatePredictionsT1();
    this.delay(100).then(() => {
      this.associatePredictionsT2();

      // Verificar que campo de MVP se haya llenado

      //Verificar goles 
      if (!(this.goalCount(this.goalsT1) == this.newPrediction.goalsT1 && this.goalCount(this.goalsT2) == this.newPrediction.goalsT2)){
        this.openError(
          'No hay coherencia entre goles de jugadores y marcador',
          'Ingrese una predicción coherente'
        );
      }

      // Verificar asistencias
      else if (!(this.assistCount(this.assistsT1) <= this.newPrediction.goalsT1 && this.assistCount(this.assistsT2) <= this.newPrediction.goalsT2)) {
        this.openError(
          'No hay coherencia entre asistencia de jugadores y marcador',
          'Ingrese una predicción coherente'
        );
      } else {
        this.newPrediction.predictionPlayer = this.allPreditions;
        console.log(this.newPrediction);
      }
    });
  }
}
