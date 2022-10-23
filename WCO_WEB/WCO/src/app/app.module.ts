import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { MatchDetailsComponent } from './components/match-details/match-details.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateTournamentComponent,
    TournamentsComponent,
    MatchDetailsComponent,
  ],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
