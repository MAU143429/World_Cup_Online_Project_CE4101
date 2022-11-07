import { Component, OnInit } from '@angular/core';
import { InternalService } from '../../services/internal.service';

@Component({
  selector: 'app-match-details',
  templateUrl: './match-details.component.html',
  styleUrls: ['./match-details.component.css'],
})
export class MatchDetailsComponent implements OnInit {
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
          name: 'Brasil',
        },
        {
          teId: '2',
          name: 'Costa Rica',
        },
      ],
    },
  ];

  /**
  allPlayers = [
    {
      teams1: ["Jugador 1.1","Jugador 1.2","Jugador 1.3"],
      teams2: [
        {
          name: 'Jugador 1.1',
        },
        {
          name: 'Jugador 1.2',
        },
      ],
    },
  ];*/
  playersteam1 = [
    {
      name: 'Jugador 1.1',
    },
    {
      name: 'Jugador 1.2',
    },
    {
      name: 'Jugador 1.3',
    },
  ];
  playersteam2 = [
    {
      name: 'Jugador 2.1',
    },
    {
      name: 'Jugador 2.2',
    },
    {
      name: 'Jugador 2.3',
    },
  ];

  players = ['Neymar Jr', 'Cristiano Ronaldo', 'Lionel Messi'];

  constructor(private connection: InternalService) {}

  ngOnInit(): void {
    this.connection.currentMatch.subscribe((data) => console.log(data));
  }
}
