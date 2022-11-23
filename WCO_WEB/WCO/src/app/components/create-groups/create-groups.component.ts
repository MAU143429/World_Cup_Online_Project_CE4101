import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-groups',
  templateUrl: './create-groups.component.html',
  styleUrls: ['./create-groups.component.css'],
})
export class CreateGroupsComponent implements OnInit {
  allTournaments = [
    {
      text: 'Champions League #234352',
      value: 234352,
    },
  ];

  constructor() {}

  ngOnInit(): void {}
}
