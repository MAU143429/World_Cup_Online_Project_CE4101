import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.css'],
})
export class ScoresComponent implements OnInit {
  allScores = [
    {
      nickname: 'CarlitosHD',
      score: '15',
    },
    {
      nickname: 'MarquitosHD',
      score: '11',
    },
    {
      nickname: 'PedritoHD',
      score: '9',
    },
  ];

  allTournaments = [
    {
      text: 'Champions League #234352',
      value: 234352,
    },
  ];
  constructor() {}

  ngOnInit(): void {}
}
