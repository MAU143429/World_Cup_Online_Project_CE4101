import { Component, OnInit } from '@angular/core';
import { TournamentService } from '../../services/tournament.service';
import { Tournaments } from '../../interface/tournaments';

@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css'],
})
export class TournamentsComponent implements OnInit {
  tournamentsData: Tournaments[] | undefined;

  tournamentsData2: Tournaments[] = [
    {
      id: '9e367a39',
      name: 'Champions League',
      startDate: '2022-10-25',
      endDate: '2022-11-03',
      description: 'Organizado por X-FIFA',
      type: 'Selecciones',
      teams: ['Brasil', 'Argentina', 'Colombia', 'Portugal'],
      brackets: ['Cuartos de final', 'Final'],
    },
    {
      id: '9e367a39',
      name: 'Champions League',
      startDate: '2022-10-25',
      endDate: '2022-11-03',
      description: 'Organizado por X-FIFA',
      type: 'Selecciones',
      teams: ['Brasil', 'Argentina', 'Colombia', 'Portugal'],
      brackets: ['Cuartos de final', 'Final'],
    },
    {
      id: '9e367a39',
      name: 'Champions League',
      startDate: '2022-10-25',
      endDate: '2022-11-03',
      description: 'Organizado por X-FIFA',
      type: 'Selecciones',
      teams: ['Brasil', 'Argentina', 'Colombia', 'Portugal'],
      brackets: ['Cuartos de final', 'Final'],
    },
    {
      id: '9e367a39',
      name: 'Champions League',
      startDate: '2022-10-25',
      endDate: '2022-11-03',
      description: 'Organizado por X-FIFA',
      type: 'Selecciones',
      teams: ['Brasil', 'Argentina', 'Colombia', 'Portugal'],
      brackets: ['Cuartos de final', 'Final'],
    },
  ];

  constructor(private service: TournamentService) {}

  ngOnInit(): void {}
}
