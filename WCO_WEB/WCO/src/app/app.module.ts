import { MbscModule } from '@mobiscroll/angular';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';

import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DialogBoxComponent } from './components/dialog-box/dialog-box.component';
import { TournamentDetailsComponent } from './components/tournament-details/tournament-details.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateMatchComponent } from './components/create-match/create-match.component';
import { MatchDetailsComponent } from './components/match-details/match-details.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ScoresComponent } from './components/scores/scores.component';
import { GroupsComponent } from './components/groups/groups.component';
import { GroupDetailsComponent } from './components/group-details/group-details.component';
import { CreateGroupsComponent } from './components/create-groups/create-groups.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateTournamentComponent,
    TournamentsComponent,
    NavbarComponent,
    DialogBoxComponent,
    TournamentDetailsComponent,
    CreateMatchComponent,
    MatchDetailsComponent,
    RegisterComponent,
    LoginComponent,
    ScoresComponent,
    GroupsComponent,
    GroupDetailsComponent,
    CreateGroupsComponent,
  ],
  imports: [
    MbscModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatInputModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgbModule,
    MatSnackBarModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
