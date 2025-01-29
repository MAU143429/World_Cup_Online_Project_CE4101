import { ConnectedOverlayPositionChange } from '@angular/cdk/overlay';
import { Component, OnInit } from '@angular/core';
import { TournamentDropdown } from 'src/app/model/tournament-dropdown';
import { AccountService } from 'src/app/services/account.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { Scores } from '../../interface/scores';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.css'],
})
export class ScoresComponent implements OnInit {
  currentTID = '';
  currentUser = -1;
  allScores: Scores[] = [];
  allTournaments: TournamentDropdown[] = [];
  constructor(
    private service: TournamentService,
    private service1: AccountService
  ) {}

  ngOnInit(): void {
    this.service.getTournaments().subscribe((data) => {
      var tournaments: TournamentDropdown[] = [];
      data.forEach((value: any) => {
        var dropdownObject: TournamentDropdown = { text: '', value: '' };
        dropdownObject.text = value.name + ' ' + value.toId;
        dropdownObject.value = value.toId;
        tournaments.push(dropdownObject);
      });

      this.allTournaments = tournaments;
    });
  }

  showScore() {
    this.service1.getTournamentScore(this.currentTID).subscribe((data) => {
      this.allScores = data;
      for (let i = 0; i < data.length; i++) {
        if (data[i].acc_nick == localStorage.getItem('nickname')) {
          this.currentUser = i;
        }
      }
    });
  }
}
