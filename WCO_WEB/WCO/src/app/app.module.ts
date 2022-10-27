import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { MatchDetailsComponent } from './components/match-details/match-details.component';
import { MatchesComponent } from './components/matches/matches.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateTournamentComponent,
    TournamentsComponent,
    MatchDetailsComponent,
    MatchesComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})

/**
 * @NgModule({
  imports: [
    [...]
    FormsModule
  ],
  [...]
})
 */
export class AppModule {}
