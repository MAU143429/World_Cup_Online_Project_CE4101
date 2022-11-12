import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateMatchComponent } from './create-match.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { dateTimeLocale } from '@mobiscroll/angular/dist/js/core/util/datetime';


describe('CreateMatchComponent', () => {
  let component: CreateMatchComponent;
  let fixture: ComponentFixture<CreateMatchComponent>;
  let httpMock: HttpTestingController;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateMatchComponent ],
      imports: [ HttpClientTestingModule, MatSnackBarModule ]
    })
    .compileComponents();

  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateMatchComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject( HttpTestingController );
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
});
