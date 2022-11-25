import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'

import { AccountService } from '../../src/app/services/account.service';
import { LoginComponent } from '../../src/app/components/login/login.component';
import { TournamentsComponent } from '../../src/app/components/tournaments/tournaments.component'
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EventManager } from '@angular/platform-browser';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable, observable, of } from 'rxjs';

const dummyAccount = [{
  email: 'prueba@gmail.com',
  password: 'lunes233' 
}];

const mockAccountService: {
  createAccount: () => Observable<any>,
  getAccountByEmail: () => Observable<any>,
  newLogin: () => Observable<any>,
  getRole: () => Observable<any>
} = {
  createAccount: () => of(dummyAccount),
  getAccountByEmail: () => of([{
    name: 'David',
    lastname: 'Soto',
    email: 'prueba@gmail.com',
    password: 'lunes233',
    nickname: 'dasotovid',
    birthdate: '01/01/01',
    country: 'Chile'
  }]),
  newLogin: () => of(dummyAccount),
  getRole: () => of(true)
};

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let compiled: HTMLElement;
  let service: AccountService;
  let httpMock: HttpTestingController;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [ 
        HttpClientTestingModule, 
        MatSnackBarModule, 
        FormsModule, 
        ReactiveFormsModule,
        BrowserAnimationsModule,
        NoopAnimationsModule,
        RouterTestingModule.withRoutes([{path: 'home', component: TournamentsComponent}])
        ],
      providers: [ 
        { provide: AccountService, useValue: mockAccountService }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    service = TestBed.inject( AccountService );
    httpMock = TestBed.inject( HttpTestingController );
    router = TestBed.inject(Router);

    fixture.detectChanges();
    compiled = fixture.nativeElement;
  });

  test('should create', () => {
    expect(component).toBeTruthy();
  });

  test('debe de hace match con el snapshot', () => {
    expect(compiled.innerHTML).toMatchSnapshot();
  });

  test('se llama addLogin() al presionar boton de ingresar', () => {
    const btnLogin = compiled.querySelector('[data-test=btn-login]');
    const spy = jest.spyOn( component, 'addLogin') ;
    btnLogin?.dispatchEvent( new Event( 'click' ) );
    console.log(btnLogin);
    expect(spy).toHaveBeenCalled();
  });

  test('sirve input de login', () => {
    const emailInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#emailInput');
    emailInput.value = 'prueba@gmail.com'
    emailInput.dispatchEvent(new Event('input'));

    const passwordInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#passwordInput');
    passwordInput.value = 'lunes233'
    passwordInput.dispatchEvent(new Event('input'));

    console.log("SIRVE ",component.newLogin.email, component.newLogin.password);

    expect( component.newLogin.email = emailInput.value);
    expect( component.newLogin.password = passwordInput.value);
    
  });

  test( 'faltan espacios requeridos', fakeAsync(() => {
    const spy = jest.spyOn( component, 'openError').mockImplementation();
    //const btnLogin = compiled.querySelector('[data-test=btn-login]');

    const emailInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#emailInput');
    emailInput.value = 'prueba@gmail.com'
    emailInput.dispatchEvent(new Event('input'));

    component.addLogin();
    tick(50);
    
    expect(spy).toHaveBeenCalled();
    
    
  }));
  
  test('deberia verificar datos y hacer route a home', fakeAsync(() => {

    const getAccSpy = jest.spyOn(service, "getAccountByEmail");
    getAccSpy.mockReturnValue(of([{
      name: 'David',
      lastname: 'Soto',
      email: 'prueba@gmail.com',
      password: 'lunes233',
      nickname: 'dasotovid',
      birthdate: '01/01/01',
      country: 'Chile',
    }]));

    const newLogInSpy = jest.spyOn(mockAccountService, "newLogin")
    newLogInSpy.mockReturnValue(of(dummyAccount));

    const getRoleSpy = jest.spyOn(mockAccountService, "getRole");
    getRoleSpy.mockReturnValue(of(true))

    const routeSpy = jest.spyOn(router, "navigate");


    const emailInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#emailInput');
    emailInput.value = 'prueba@gmail.com'
    emailInput.dispatchEvent(new Event('input'));

    const pwInput: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#passwordInput');
    pwInput.value = 'lunes233'
    pwInput.dispatchEvent(new Event('input'));

    component.addLogin();
    tick(800);

    console.log(component.Users[0])
    expect(routeSpy).toHaveBeenCalled();

    // revisar que se haga el route a donde sea que ocupe
  }));
  
});
