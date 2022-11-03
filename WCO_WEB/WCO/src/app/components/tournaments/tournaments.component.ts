import { Component, OnInit } from '@angular/core';
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

    this.service.getTournaments().subscribe((data) => console.log(data));
  }

  openTournament(id: String) {
    this.connection.sendTournamentKey(id);
  }
}
