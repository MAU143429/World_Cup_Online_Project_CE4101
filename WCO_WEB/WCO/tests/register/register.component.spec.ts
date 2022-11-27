import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { RegisterComponent } from '../../src/app/components/register/register.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { AccountService } from '../../src/app/services/account.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable, observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { FormsModule, NgModel, NgSelectOption, ReactiveFormsModule } from '@angular/forms';
import { EventManager } from '@angular/platform-browser';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from '../../src/app/components/login/login.component';


const dummyAccount = [{
  name: 'David',
  lastname: 'Soto',
  email: 'prueba@gmail.com',
  password: 'lunes233',
  nickname: 'dasotovid',
  birthdate: '01/01/2001',
  country: 'Chile'
}];

const mockAccountService: {
  createAccount: () => Observable<any>,
  getAccountByEmail: () => Observable<any>,
  getAccountByNickname: () => Observable<any>
} = {
  createAccount: () => of(dummyAccount),
  getAccountByEmail: () => of([]),
  getAccountByNickname: () => of([])
};


describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let compiled: HTMLElement;
  let service: AccountService;
  let httpMock: HttpTestingController;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterComponent ],
      imports: [ 
        HttpClientTestingModule, 
        MatSnackBarModule,
        //FormsModule, 
        //ReactiveFormsModule,
        
        BrowserAnimationsModule,
        NoopAnimationsModule,
        RouterTestingModule.withRoutes([{path: 'login', component: LoginComponent}])
        ],
      providers: [ 
        { provide: AccountService, useValue: mockAccountService }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterComponent);
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

  test('Valid email', () => {
    let test = component.checkEmailFormat("usuario@gmail.com")
    expect(test).toBeTruthy();
  })

  test('Invalid email', () => {
    let test = component.checkEmailFormat("usuario2gmail.com")
    expect(test).toBeFalsy();
  })

  test('Valid password', () => {
    let test = component.checkPasswordFormat("AA8POO")
    expect(test).toBeTruthy();
  })

  test('Invalid password', () => {
    let test = component.checkPasswordFormat("121456")
    expect(test).toBeFalsy();
  })

  test('Age 22 true', () => {
    component.newAccount.birthdate = '06/06/2000'
    component.CalculateAge();
    expect(component.age).toBe(22);
  })

  test('Age 19 false', () => {
    component.newAccount.birthdate = '06/06/2000'
    component.CalculateAge();
    expect(component.age == 19).toBeFalsy();
  })

  test('Change checkbox status to true', () => {
    component.changeStatus();
    expect(component.checkboxStatus).toBeTruthy();
  })

  test('Change checkbox status to false', () => {
    component.changeStatus();
    component.changeStatus();
    expect(component.checkboxStatus).toBeFalsy();
  })

  test('se prueba addUser()', fakeAsync(() => {
    const getAccESpy = jest.spyOn(service, "getAccountByEmail");
    getAccESpy.mockReturnValue(of([]));

    const getAccNSpy = jest.spyOn(service, "getAccountByNickname");
    getAccNSpy.mockReturnValue(of([]));

    const createAccSpy = jest.spyOn(service, 'createAccount');
    createAccSpy.mockReturnValue(of(dummyAccount));

    const routeSpy = jest.spyOn(router, 'navigate');

    component.newAccount.email = 'prueba@gmail.com'
    component.newAccount.password = 'lunes233'
    component.newAccount.name = 'David'
    component.newAccount.lastname = 'Soto'
    component.newAccount.nickname = 'dasotovid'
    component.newAccount.birthdate = "01/01/2001"
    component.newAccount.country = "Chile"
    component.checkboxStatus = true;

    fixture.detectChanges();

    component.addUser();
    tick(300);

    expect(routeSpy).toHaveBeenCalledWith(["/login"]);
  }))


  test('se prueba addUser() sin ingresar correo', fakeAsync(() => {
    const getAccESpy = jest.spyOn(service, "getAccountByEmail");
    getAccESpy.mockReturnValue(of([]));

    const getAccNSpy = jest.spyOn(service, "getAccountByNickname");
    getAccNSpy.mockReturnValue(of([]));

    const createAccSpy = jest.spyOn(service, 'createAccount');
    createAccSpy.mockReturnValue(of(dummyAccount));

    const errorSpy = jest.spyOn(component, 'openError');

    component.newAccount.email = ''
    component.newAccount.password = 'lunes233'
    component.newAccount.name = 'David'
    component.newAccount.lastname = 'Soto'
    component.newAccount.nickname = 'dasotovid'
    component.newAccount.birthdate = "01/01/2001"
    component.newAccount.country = "Chile"
    component.checkboxStatus = true;

    fixture.detectChanges();

    component.addUser();
    tick(300);

    expect(errorSpy).toHaveBeenCalledWith("Faltan espacios requeridos para registrarse", "Intente de nuevo");
  }))

  test('se prueba addUser() sin aceptar TyC', fakeAsync(() => {
    const getAccESpy = jest.spyOn(service, "getAccountByEmail");
    getAccESpy.mockReturnValue(of([]));

    const getAccNSpy = jest.spyOn(service, "getAccountByNickname");
    getAccNSpy.mockReturnValue(of([]));

    const createAccSpy = jest.spyOn(service, 'createAccount');
    createAccSpy.mockReturnValue(of(dummyAccount));

    const errorSpy = jest.spyOn(component, 'openError');

    component.newAccount.email = 'prueba@gmail.com'
    component.newAccount.password = 'lunes233'
    component.newAccount.name = 'David'
    component.newAccount.lastname = 'Soto'
    component.newAccount.nickname = 'dasotovid'
    component.newAccount.birthdate = "01/01/2001"
    component.newAccount.country = "Chile"
    component.checkboxStatus = false;

    fixture.detectChanges();

    component.addUser();
    tick(300);

    expect(errorSpy).toHaveBeenCalledWith("Debe aceptar términos y condiciones", "Volver");
  }))

  test('se prueba addUser() con formato incorrecto de contraseña', fakeAsync(() => {
    const getAccESpy = jest.spyOn(service, "getAccountByEmail");
    getAccESpy.mockReturnValue(of([]));

    const getAccNSpy = jest.spyOn(service, "getAccountByNickname");
    getAccNSpy.mockReturnValue(of([]));

    const createAccSpy = jest.spyOn(service, 'createAccount');
    createAccSpy.mockReturnValue(of(dummyAccount));

    const errorSpy = jest.spyOn(component, 'openError');

    component.newAccount.email = 'prueba@gmail.com'
    component.newAccount.password = 'lunes'
    component.newAccount.name = 'David'
    component.newAccount.lastname = 'Soto'
    component.newAccount.nickname = 'dasotovid'
    component.newAccount.birthdate = "01/01/2001"
    component.newAccount.country = "Chile"
    component.checkboxStatus = true;

    fixture.detectChanges();

    component.addUser();
    tick(300);

    expect(errorSpy).toHaveBeenCalledWith('Formato de contraseña inválido, debe ser alfanumérica',
    'Intente de nuevo');
  }))

  test('se prueba addUser() con formato de contraseña eróoneo', fakeAsync(() => {
    const getAccESpy = jest.spyOn(service, "getAccountByEmail");
    getAccESpy.mockReturnValue(of([]));

    const getAccNSpy = jest.spyOn(service, "getAccountByNickname");
    getAccNSpy.mockReturnValue(of([]));

    const createAccSpy = jest.spyOn(service, 'createAccount');
    createAccSpy.mockReturnValue(of(dummyAccount));

    const errorSpy = jest.spyOn(component, 'openError');

    component.newAccount.email = 'prueba@gmail.com'
    component.newAccount.password = 'pt5'
    component.newAccount.name = 'David'
    component.newAccount.lastname = 'Soto'
    component.newAccount.nickname = 'dasotovid'
    component.newAccount.birthdate = "01/01/2001"
    component.newAccount.country = "Chile"
    component.checkboxStatus = true;

    fixture.detectChanges();

    component.addUser();
    tick(300);

    expect(errorSpy).toHaveBeenCalledWith('La contraseña debe tener una extensión entre 6 y 8 caractéres',
    'Intente de nuevo');
  }))


  test('se prueba addUser() con menor de 18 años', fakeAsync(() => {
    const getAccESpy = jest.spyOn(service, "getAccountByEmail");
    getAccESpy.mockReturnValue(of([]));

    const getAccNSpy = jest.spyOn(service, "getAccountByNickname");
    getAccNSpy.mockReturnValue(of([]));

    const createAccSpy = jest.spyOn(service, 'createAccount');
    createAccSpy.mockReturnValue(of(dummyAccount));

    const errorSpy = jest.spyOn(component, 'openError');

    component.newAccount.email = 'prueba@gmail.com'
    component.newAccount.password = 'lunes223'
    component.newAccount.name = 'David'
    component.newAccount.lastname = 'Soto'
    component.newAccount.nickname = 'dasotovid'
    component.newAccount.birthdate = "01/01/2011"
    component.newAccount.country = "Chile"
    component.checkboxStatus = true;

    fixture.detectChanges();

    component.addUser();
    tick(300);

    expect(errorSpy).toHaveBeenCalledWith('Debe ser mayor de 18 años', 'Intente de nuevo');
  }))


});
