import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterComponent } from './register.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { AccountService } from '../../services/account.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let compiled: HTMLElement;
  let service: AccountService;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterComponent ],
      imports: [ HttpClientTestingModule, MatSnackBarModule ],
      providers: [ AccountService ]
    })
    .compileComponents();

  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    service = TestBed.inject( AccountService );
    httpMock = TestBed.inject( HttpTestingController );

    fixture.detectChanges();
    compiled = fixture.nativeElement;
  });

  test('should create', () => {
    expect(component).toBeTruthy();
  });

  it('Valid email', () => {
    let test = component.checkEmailFormat("usuario@gmail.com")
    expect(test).toBeTruthy();
  })

  it('Invalid email', () => {
    let test = component.checkEmailFormat("usuario2gmail.com")
    expect(test).toBeFalsy();
  })

  it('Valid password', () => {
    let test = component.checkPasswordFormat("AA8POO")
    expect(test).toBeTruthy();
  })

  it('Invalid password', () => {
    let test = component.checkPasswordFormat("121456")
    expect(test).toBeFalsy();
  })

  it('Age 22 true', () => {
    component.newAccount.birthdate = '06/06/2000'
    component.CalculateAge();
    expect(component.age).toBe(22);
  })

  it('Age 19 false', () => {
    component.newAccount.birthdate = '06/06/2000'
    component.CalculateAge();
    expect(component.age == 19).toBeFalsy();
  })

  it('Change checkbox status to true', () => {
    component.changeStatus();
    expect(component.checkboxStatus).toBeTruthy();
  })

  it('Change checkbox status to false', () => {
    component.changeStatus();
    component.changeStatus();
    expect(component.checkboxStatus).toBeFalsy();
  })

});
