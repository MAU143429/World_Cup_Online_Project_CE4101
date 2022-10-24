import { MatchesComponent } from './components/matches/matches.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { MatchDetailsComponent } from './components/match-details/match-details.component';

const routes: Routes = [
  { path: 'create-tournaments', component: CreateTournamentComponent },
  { path: '', component: TournamentsComponent },
  { path: 'match-details', component: MatchDetailsComponent },
  { path: 'matches', component: MatchesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
