import { Component, OnInit } from '@angular/core';
import { TournamentService } from '../../services/tournament.service';
import { Tournaments } from '../../interface/tournaments';
import { InternalService } from '../../services/internal.service';
import { BracketName } from '../../interface/bracket-name';
@Component({
  selector: 'app-tournament-details',
  templateUrl: './tournament-details.component.html',
  styleUrls: ['./tournament-details.component.css'],
})
export class TournamentDetailsComponent implements OnInit {
  tournamentsData: Tournaments[] = [];
  bracketsData: String[] = [];

  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  constructor(
    private service: TournamentService,
    private connection: InternalService
  ) {}

  ngOnInit(): void {
    this.service
      .getTournamentbyID(this.connection.getTournamentKey())
      .subscribe((data) => (this.tournamentsData = data));

    this.delay(100).then(() => {
      this.bracketsData = this.tournamentsData[0].brackets;
      console.log(this.tournamentsData);
    });
  }
}
