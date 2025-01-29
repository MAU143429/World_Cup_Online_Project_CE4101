import { TournamentService } from '../../services/tournament.service';
import { Tournaments } from '../../interface/tournaments';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css'],
})
export class TournamentsComponent implements OnInit {
  tournamentsData: Tournaments[] = [];
  scope: any;
  constructor(private service: TournamentService) {}

  ngOnInit(): void {
    this.service.getTournaments().subscribe((data: Tournaments[]) => {
      this.tournamentsData = data;
    });
    this.scope = localStorage.getItem('scope');
  }

  /**
   * Este metodo permite guardar la llave de torneo para que luego este
   * se puede abrir desde la vista detallada.
   */
  openTournament(id: string) {
    localStorage.setItem('toID', id);
  }
}
