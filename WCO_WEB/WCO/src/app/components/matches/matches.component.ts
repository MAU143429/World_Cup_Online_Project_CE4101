import { MatchesService } from './../../services/matches.service';
import { Match } from './../../model/match';
import { Component, OnInit, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import { JsonPipe } from '@angular/common';
@Component({
  selector: 'app-matches',
  template: `
  Match
`,
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {
  newMatch: Match = new Match
  //selectionsList: 
  constructor(private game:MatchesService) {  }

  ngOnInit(): void {

  }

  insertMatch(newMatch: Match){
    console.log(JSON.stringify(newMatch))
    this.game.addNewMatch(newMatch).subscribe((match)=>
    console.log(this.newMatch)
    )
  }

}
