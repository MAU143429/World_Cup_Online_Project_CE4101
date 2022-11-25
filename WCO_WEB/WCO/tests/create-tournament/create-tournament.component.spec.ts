import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateTournamentComponent } from '../../src/app/components/create-tournament/create-tournament.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TournamentService } from '../../src/app/services/tournament.service';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationPlayer } from '@angular/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable, of } from 'rxjs';
import { MatDialogModule } from '@angular/material/dialog';
import fi from '@mobiscroll/angular/dist/js/i18n/fi';

const dummyTournament = [{
  name: 'Mundial',
  startDate: '10/10/2022',
  endDate: '11/11/2022',
  type: 'Selecciones',
  description: 'Woww mundial',
  teams: [{name: "T1", id: 1}, {name: "TB", id: 2}],
  brackets: ["Final"]
}]
const mockTournamentService: {
  getTeamsbyType: () => Observable<any>,
  setTournament: () => Observable<any>
} = {
  getTeamsbyType: () => of([{name: "T1", id: 1}, {name: "TB", id: 2}]),
  setTournament: () => of(dummyTournament)
};


describe('CreateTournamentComponent', () => {
  let component: CreateTournamentComponent;
  let fixture: ComponentFixture<CreateTournamentComponent>;
  let compiled: HTMLElement;
  let httpMock: HttpTestingController;
  let service: TournamentService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTournamentComponent ],
      imports: [ 
        HttpClientTestingModule, 
        MatSnackBarModule,
        //FormsModule,
        //ReactiveFormsModule,
        MatDialogModule,
        NoopAnimationsModule
      ],
      providers: [
        {provide: TournamentService, useValue: mockTournamentService}
      ]
    })
    .compileComponents();
    
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTournamentComponent);
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

  test("getSelectedItem()", () => {
    component.radioSelected = 'Selecciones';

    const getTeamsSpy = jest.spyOn(mockTournamentService, "getTeamsbyType")
    getTeamsSpy.mockReturnValue(of([{name: "T1", teId: 1}, {name: "TB", teId: 2}]));

    component.getSelecteditem();
    expect(component.dropdownData).toEqual([{text: "T1", value: 1}, {text: "TB", value: 2}])
  })

  test('deberia obtener nombres de brackets en formato correcto para API usando setBracketList()', () =>{
    component.bracketSource = [{name: "Final"}]
    component.setBracketList();
    fixture.detectChanges()

    expect(component.newTournament.brackets).toEqual(["Final"]);
  })

  test('Se deberia agregar torneo, addTournament()', () => {

    jest.useFakeTimers().setSystemTime(new Date('2020-01-01'));

    const setTournamentSpy = jest.spyOn(mockTournamentService, 'setTournament')
    setTournamentSpy.mockReturnValue(of(dummyTournament));

    const errorSpy = jest.spyOn( component, 'openError')

    component.newTournament.endDate = '11/11/2022';
    component.newTournament.name = 'Mundial';
    component.newTournament.startDate = '10/10/2022';
    component.newTournament.teams.length = 2;
    component.newTournament.brackets.length = 1;

    component.addTournament();
    expect(setTournamentSpy).toHaveBeenCalled();
  })

  test('Error por fecha inicio depues de fecha fin, addTournament()', () => {

    jest.useFakeTimers().setSystemTime(new Date('2020-01-01'));

    const setTournamentSpy = jest.spyOn(mockTournamentService, 'setTournament')
    setTournamentSpy.mockReturnValue(of(dummyTournament));

    const errorSpy = jest.spyOn( component, 'openError')

    component.newTournament.endDate = '11/11/2022';
    component.newTournament.name = 'Mundial';
    component.newTournament.startDate = '12/12/2022';
    component.newTournament.teams.length = 2;
    component.newTournament.brackets.length = 1;

    component.addTournament();
    expect(errorSpy).toHaveBeenCalledWith('Error en fechas ingresadas!', 'Fecha final incorrecta!');
  })

  test('Error por falta de equipos, addTournament()', () => {

    jest.useFakeTimers().setSystemTime(new Date('2020-01-01'));

    const setTournamentSpy = jest.spyOn(mockTournamentService, 'setTournament')
    setTournamentSpy.mockReturnValue(of(dummyTournament));

    const errorSpy = jest.spyOn( component, 'openError')

    component.newTournament.endDate = '11/11/2022';
    component.newTournament.name = 'Mundial';
    component.newTournament.startDate = '10/10/2022';
    component.newTournament.teams.length = 1;
    component.newTournament.brackets.length = 1;

    component.addTournament();
    expect(errorSpy).toHaveBeenCalledWith('Se requiere al menos dos equipos para crear un torneo!',
    'Ingrese algún otro antes de continuar!');
  })

  test('Error por fecha inicio en el pasado, addTournament()', () => {

    jest.useFakeTimers().setSystemTime(new Date('2020-01-01'));

    const setTournamentSpy = jest.spyOn(mockTournamentService, 'setTournament')
    setTournamentSpy.mockReturnValue(of(dummyTournament));

    const errorSpy = jest.spyOn( component, 'openError')

    component.newTournament.endDate = '11/11/2022';
    component.newTournament.name = 'Mundial';
    component.newTournament.startDate = '12/12/2019';
    component.newTournament.teams.length = 2;
    component.newTournament.brackets.length = 1;

    component.addTournament();
    expect(errorSpy).toHaveBeenCalledWith('Error no se pueden generar torneos en el pasado!',
    'Ingrese una fecha correcta para continuar!');
  })

  test('Inputs funcionan', () => {

    const nameInput1: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#nameInput');
    nameInput1.value = 'Mundial 1';
    nameInput1.dispatchEvent(new Event('input'));

    expect(component.newTournament.name == nameInput1.value)

    const startDateInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#startDateInput');
    startDateInput.value = '10/10/2022'
    startDateInput.dispatchEvent(new Event('input'));

    expect(component.newTournament.startDate).toEqual(startDateInput.value)

    const endDateInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#endDateInput');
    endDateInput.value = '11/11/2022'
    endDateInput.dispatchEvent(new Event('input'));

    expect(component.newTournament.endDate).toEqual(endDateInput.value)

    const dot1: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#dot-1');
    dot1.dispatchEvent(new Event( 'click' ))

    expect(component.newTournament.type == dot1.value)
  })

});
