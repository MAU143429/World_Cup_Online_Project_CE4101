import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentDetailsComponent } from './components/tournament-details/tournament-details.component';
import { CreateMatchComponent } from './components/create-match/create-match.component';

const routes: Routes = [
  { path: '', component: TournamentsComponent },
  { path: 'create-tournaments', component: CreateTournamentComponent },
  { path: 'tournament-details', component: TournamentDetailsComponent },
  { path: 'create-match', component: CreateMatchComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
