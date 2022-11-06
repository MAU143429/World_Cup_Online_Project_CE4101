import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentDetailsComponent } from './components/tournament-details/tournament-details.component';
import { CreateMatchComponent } from './components/create-match/create-match.component';
import { MatchDetailsComponent } from './components/match-details/match-details.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: 'home', component: TournamentsComponent },
  { path: 'create-tournaments', component: CreateTournamentComponent },
  { path: 'tournament-details', component: TournamentDetailsComponent },
  { path: 'create-match', component: CreateMatchComponent },
  { path: 'match-details', component: MatchDetailsComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
