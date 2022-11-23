import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentDetailsComponent } from './components/tournament-details/tournament-details.component';
import { CreateMatchComponent } from './components/create-match/create-match.component';
import { MatchDetailsComponent } from './components/match-details/match-details.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { ScoresComponent } from './components/scores/scores.component';
import { GroupsComponent } from './components/groups/groups.component';
import { GroupDetailsComponent } from './components/group-details/group-details.component';
import { CreateGroupsComponent } from './components/create-groups/create-groups.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: TournamentsComponent },
  { path: 'scores', component: ScoresComponent },
  { path: 'groups', component: GroupsComponent },
  { path: 'create-groups', component: CreateGroupsComponent },
  { path: 'group-details', component: GroupDetailsComponent },
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
