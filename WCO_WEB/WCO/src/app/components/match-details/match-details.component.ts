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
          name: 'Argentina',
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
    {
      name: 'Jugador 1.4',
    },
    {
      name: 'Jugador 1.5',
    },
    {
      name: 'Jugador 1.6',
    },
    {
      name: 'Jugador 1.7',
    },
    {
      name: 'Jugador 1.8',
    },
    {
      name: 'Jugador 1.9',
    },
    {
      name: 'Jugador 1.10',
    },
    {
      name: 'Jugador 1.11',
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
    {
      name: 'Jugador 2.4',
    },
    {
      name: 'Jugador 2.5',
    },
    {
      name: 'Jugador 2.6',
    },
    {
      name: 'Jugador 2.7',
    },
    {
      name: 'Jugador 2.8',
    },
    {
      name: 'Jugador 2.9',
    },
    {
      name: 'Jugador 2.10',
    },
    {
      name: 'Jugador 2.11',
    },
  ];

  players = ['Neymar Jr', 'Cristiano Ronaldo', 'Lionel Messi'];

  constructor(private connection: InternalService) {}

  ngOnInit(): void {
    this.connection.currentMatch.subscribe((data) => console.log(data));
  }
}
