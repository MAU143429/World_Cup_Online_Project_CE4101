import { Component, OnInit } from '@angular/core';
import { Scores } from 'src/app/interface/scores';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.css'],
})
export class GroupDetailsComponent implements OnInit {
  allScores: Scores[] = [];
  groupInfo: any;

  constructor(private service: AccountService) {}

  ngOnInit(): void {
    this.service
      .getGroupScore(localStorage.getItem('gId'))
      .subscribe((data) => (this.allScores = data));

    this.service
      .getGroupInfo(localStorage.getItem('gId'))
      .subscribe((data) => (this.groupInfo = data));
  }
}
