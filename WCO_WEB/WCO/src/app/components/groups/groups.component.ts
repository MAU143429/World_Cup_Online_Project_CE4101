import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css'],
})
export class GroupsComponent implements OnInit {
  groupsData = [
    {
      name: 'Los Compas',
      gId: '5423JKD3D3F55FS4',
      t_name: 'Champions League',
    },
  ];
  constructor() {}

  ngOnInit(): void {}
}
