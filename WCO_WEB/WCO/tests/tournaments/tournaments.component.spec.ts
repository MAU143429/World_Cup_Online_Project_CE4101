import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
//import { http } from '@mobiscroll/angular/dist/js/core/util/http';
import { Observable, of } from 'rxjs';
import { TournamentService } from '../../src/app/services/tournament.service';

import { TournamentsComponent } from '../../src/app/components/tournaments/tournaments.component';

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

const mockTournamentService: {
  getTournaments: () => Observable<any>
} = {
  getTournaments: () => of(dummyTournament)
};


describe('TournamentsComponent', () => {
  let component: TournamentsComponent;
  let fixture: ComponentFixture<TournamentsComponent>;
  let compiled: HTMLElement;
  let httpMock: HttpTestingController;
  let service: TournamentService;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TournamentsComponent ],
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        {provide: TournamentService, useValue: mockTournamentService}
      ]
    })
    .compileComponents();
  
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TournamentsComponent);
    component = fixture.componentInstance;
    service = TestBed.inject( TournamentService );
    httpMock = TestBed.inject( HttpTestingController );
    compiled = fixture.nativeElement;
    fixture.detectChanges();
  });

  

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  test('debe de hace match con el snapshot', () => {
    expect(compiled.innerHTML).toMatchSnapshot();
  });

  test('En init debe traer torneo', () => {
    const setTournamentSpy = jest.spyOn(mockTournamentService, 'getTournaments')
    setTournamentSpy.mockReturnValue(of(dummyTournament));

    component.ngOnInit();
    expect(component.tournamentsData == dummyTournament)
  })

  test('openTorunament()', () => {
    component.openTournament("2")
    fixture.detectChanges();
    expect(localStorage.getItem('toID')).toEqual("2")
  });
});
