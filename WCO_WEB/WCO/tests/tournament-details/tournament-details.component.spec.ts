import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { http } from '@mobiscroll/angular/dist/js/core/util/http';
import { Observable, of } from 'rxjs';
import { TournamentService } from '../../src/app/services/tournament.service';
import { MatchesService } from '../../src/app/services/matches.service';

import { TournamentDetailsComponent } from '../../src/app/components/tournament-details/tournament-details.component';
import { NgbAccordion, NgbAccordionModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';

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
  startTime: '1:00',
  date: '11/15/2022',
  venue: 'Guapiles',
  bracketId: '0',
  idTeam1: 0,
  idTeam2: 1
}]

const mockTournamentService: {
  getTournamentbyID: () => Observable<any>
} = {
  getTournamentbyID: () => of(dummyTournament)
};

const mockMatchesService: {
  getMatchesByBracketId: () => Observable<any>
} = {
  getMatchesByBracketId: () => of(dummyMatch)
};

describe('TournamentDetailsComponent', () => {
  let component: TournamentDetailsComponent;
  let fixture: ComponentFixture<TournamentDetailsComponent>;
  let compiled: HTMLElement;
  let httpMock: HttpTestingController;
  let serviceT: TournamentService;
  let serviceM: MatchesService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TournamentDetailsComponent ],
      imports: [
        HttpClientTestingModule,
        NgbNavModule,
        NgbAccordionModule
      ],
      providers: [
        {provide: TournamentService, useValue: mockTournamentService},
        {provide: MatchesService, useValue: mockMatchesService}
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TournamentDetailsComponent);
    component = fixture.componentInstance;
    serviceT = TestBed.inject( TournamentService );
    serviceM = TestBed.inject( MatchesService );

    httpMock = TestBed.inject( HttpTestingController );
    compiled = fixture.nativeElement;
    fixture.detectChanges();
  })

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  test('debe de hace match con el snapshot', () => {
    expect(compiled.innerHTML).toMatchSnapshot();
  });

  test('En init debe traer torneo por id y partidos por id de bracket', () => {
    const getTournamentSpy = jest.spyOn(mockTournamentService, 'getTournamentbyID')
    getTournamentSpy.mockReturnValue(of(dummyTournament));

    const getMatchesSpy = jest.spyOn(mockMatchesService, 'getMatchesByBracketId')
    getMatchesSpy.mockReturnValue(of(dummyMatch));
    
    component.ngOnInit();
    expect(component.tournamentsData == dummyTournament)
    expect(component.matchData == dummyMatch)
  })

  test('debe actualizarse el valor de bracket actual usando redirectMatch', () => {
    const dummyBracket = 
    {  
      bId: 0,
      name: 'Final',
      tounamentId: 'A2X'
    }
    component.redirectMatch(dummyBracket);
    expect(localStorage.getItem("currentBracket"))
      .toEqual(dummyBracket.name)
  })

  test('debe actualizarse el valor de partido actual usando viewMatch', () => {

    component.viewMatch(JSON.stringify(dummyMatch[0]));
    const n = localStorage.getItem("currentMatch");
    expect(JSON.parse(n!))
      .toEqual(dummyMatch[0])
  })

});
