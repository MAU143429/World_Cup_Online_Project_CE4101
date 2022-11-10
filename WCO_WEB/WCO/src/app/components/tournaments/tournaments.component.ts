import { Component, Input, OnInit } from '@angular/core';
import { TournamentService } from '../../services/tournament.service';
import { InternalService } from '../../services/internal.service';
import { Tournaments } from '../../interface/tournaments';

@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css'],
})
export class TournamentsComponent implements OnInit {
  tournamentsData: Tournaments[] = [];
  constructor(
    private service: TournamentService,
    private connection: InternalService
  ) {}

  ngOnInit(): void {
    this.service.getTournaments().subscribe((data: Tournaments[]) => {
      this.tournamentsData = data;
    });
    console.log(localStorage.getItem('email'));
    console.log(localStorage.getItem('nickname'));
  }

  /**
   * Este metodo permite guardar la llave de torneo para que luego este
   * se puede abrir desde la vista detallada.
   */
  openTournament(id: string) {
    this.connection.setTournamentId(id);
  }
}
