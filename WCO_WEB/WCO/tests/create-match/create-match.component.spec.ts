import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateMatchComponent } from '../../src/app/components/create-match/create-match.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { dateTimeLocale } from '@mobiscroll/angular/dist/js/core/util/datetime';
import { Observable, of } from 'rxjs';
import { TournamentService } from '../../src/app/services/tournament.service';
import { MatchesService } from '../../src/app/services/matches.service';
import { Route, RouteConfigLoadEnd, Router } from '@angular/router';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

const dummyTournament = [{
  toId: '1',
  name: 'Mundial',
  startDate: '10/10/2022',
  endDate: '11/11/2022',
  type: 'Selecciones',
  description: 'Muy chiva',
  teams: [{teId: 0, name: 'Team1'}, {teId: 1, name: 'Team2'}],
  brackets: [{  
    bId: 0,
    name: 'Final',
    tounamentId: 'A2X'}]
}]

const dummyMatch = [{
  startTime: '16:00',
  date: '11/15/2022',
  venue: 'Guapiles',
  bracketId: '0',
  idTeam1: 0,
  idTeam2: 1
}]

//const dummyTeams = [{name: "T1", id: 1}, {name: "T2", id: 2}]

const mockTournamentService: {
  getTournamentbyID: () => Observable<any>,
  getTournamentTeams: () => Observable<any>
} = {
  getTournamentbyID: () => of(dummyTournament),
  getTournamentTeams: () => of([dummyTournament[0].teams])
}; 

const mockMatchesService: {
  addNewMatch: () => Observable<any>
} = {
  addNewMatch: () => of(dummyMatch)
};

describe('CreateMatchComponent', () => {
  let component: CreateMatchComponent;
  let fixture: ComponentFixture<CreateMatchComponent>;
  let httpMock: HttpTestingController;
  let compiled: HTMLElement;
  let serviceT: TournamentService;
  let serviceM: MatchesService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateMatchComponent ],
      imports: [ 
        HttpClientTestingModule, 
        MatSnackBarModule,
        BrowserAnimationsModule,
        NoopAnimationsModule
       ],
       providers: [
        { provide: TournamentService, useValue: mockTournamentService},
        { provide: MatchesService, useValue: mockMatchesService}
       ]
    })
    .compileComponents();

  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateMatchComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject( HttpTestingController );
    serviceM = TestBed.inject( MatchesService );
    serviceT = TestBed.inject( TournamentService );

    compiled = fixture.nativeElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('Fecha de datetime', () => {
    let date = "03/03/2002T16:00"
    expect(component.getDate(date) == "03/03/2002").toBeTruthy();
  })
  it('Hora de datetime', () => {
    let date = "03/03/2002T16:00"
    expect(component.getTime(date) == "16:00").toBeTruthy();
  })

  test('OnInit trae los datos necesarios del torneo actual para crear el partido', () => {
    const dummyBracket = {  
      bId: 0,
      name: 'Final',
      tounamentId: 'A2X'
    };
    localStorage.setItem('toID', '1');
    localStorage.setItem('currentBracket', JSON.stringify(dummyBracket));

    const getTournamentSpy = jest.spyOn(mockTournamentService, 'getTournamentbyID')
    getTournamentSpy.mockReturnValue(of(dummyTournament));

    const getTournamentTeamsSpy = jest.spyOn(mockTournamentService, 'getTournamentTeams')
    getTournamentTeamsSpy.mockReturnValue(of([dummyTournament[0].teams]));
    
    component.ngOnInit();

    expect(component.tournamentsData == dummyTournament);
    expect(component.bracket == dummyBracket);
    expect(component.allTournamentsTeams = dummyTournament[0].teams);
  });

  test('Se hace verificaciones y se inserta partido usando insertMatch()', () => {
    jest.useFakeTimers().setSystemTime( new Date('2020-01-01'));

    component.tournamentsData = dummyTournament;

    const addNewMatchSpy = jest.spyOn(mockMatchesService, 'addNewMatch');
    addNewMatchSpy.mockReturnValue(of(dummyMatch));

    const openErrorSpy = jest.spyOn(component, 'openError');
    const openSuccessSpy = jest.spyOn(component, 'openSuccess');


    component.newMatch.venue = 'Guapiles';
    component.datetime = '11/09/2022T16:00';
    component.newMatch.idTeam1 = 0;
    component.newMatch.idTeam2 = 1;

    component.insertMatch();

    expect(openSuccessSpy).toHaveBeenCalledWith('Partido creado con éxito', 'Ok');

  })

  test('falta espacio insertMatch()', () => {
    jest.useFakeTimers().setSystemTime( new Date('2020-01-01'));

    component.tournamentsData = dummyTournament;

    const addNewMatchSpy = jest.spyOn(mockMatchesService, 'addNewMatch');
    addNewMatchSpy.mockReturnValue(of(dummyMatch));

    const openErrorSpy = jest.spyOn(component, 'openError');
    const openSuccessSpy = jest.spyOn(component, 'openSuccess');

    component.newMatch.venue = '';
    component.datetime = '11/15/2022T16:00';
    component.newMatch.idTeam1 = 0;
    component.newMatch.idTeam2 = 1;

    component.insertMatch();

    expect(openErrorSpy).toHaveBeenCalledWith('Falta al menos uno de los espacios requeridos!',
    'Intente de nuevo');
  })

  test('partido en el pasado insertMatch()', () => {
    jest.useFakeTimers().setSystemTime( new Date('2020-01-01'));

    component.tournamentsData = dummyTournament;

    const addNewMatchSpy = jest.spyOn(mockMatchesService, 'addNewMatch');
    addNewMatchSpy.mockReturnValue(of(dummyMatch));

    const openErrorSpy = jest.spyOn(component, 'openError');
    const openSuccessSpy = jest.spyOn(component, 'openSuccess');

    component.newMatch.venue = 'Guapiles';
    component.datetime = '11/09/1900T16:00';
    component.newMatch.idTeam1 = 0;
    component.newMatch.idTeam2 = 1;

    component.insertMatch();

    expect(openErrorSpy).toHaveBeenCalledWith('La fecha ingresa es invalida!',
    'No se pueden crear partidos en el pasado');
  })

  test('partido en el pasado insertMatch()', () => {
    jest.useFakeTimers().setSystemTime( new Date('2020-01-01'));

    component.tournamentsData = dummyTournament;

    const addNewMatchSpy = jest.spyOn(mockMatchesService, 'addNewMatch');
    addNewMatchSpy.mockReturnValue(of(dummyMatch));

    const openErrorSpy = jest.spyOn(component, 'openError');
    const openSuccessSpy = jest.spyOn(component, 'openSuccess');

    component.newMatch.venue = 'Guapiles';
    component.datetime = '11/09/2023T16:00';
    component.newMatch.idTeam1 = 0;
    component.newMatch.idTeam2 = 1;

    component.insertMatch();

    expect(openErrorSpy).toHaveBeenCalledWith(
      'La fecha ingresa es invalida!',
      'No es posible crear el partido en fechas fuera de torneo'
    );
  })

  test('partido en el pasado insertMatch()', () => {
    jest.useFakeTimers().setSystemTime( new Date('2020-01-01'));

    component.tournamentsData = dummyTournament;

    const addNewMatchSpy = jest.spyOn(mockMatchesService, 'addNewMatch');
    addNewMatchSpy.mockReturnValue(of(dummyMatch));

    const openErrorSpy = jest.spyOn(component, 'openError');
    const openSuccessSpy = jest.spyOn(component, 'openSuccess');

    component.newMatch.venue = 'Guapiles';
    component.datetime = '11/09/2022T16:00';
    component.newMatch.idTeam1 = 1;
    component.newMatch.idTeam2 = 1;

    component.insertMatch();

    expect(openErrorSpy).toHaveBeenCalledWith(
      'Error al crear el partido!',
      'El equipo 1 y el equipo 2 son el mismo!'
    );
  })

});
