import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatchDetailsComponent } from '../../src/app/components/match-details/match-details.component';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { dateTimeLocale } from '@mobiscroll/angular/dist/js/core/util/datetime';

describe('MatchDetailsComponent', () => {
  let component: MatchDetailsComponent;
  let fixture: ComponentFixture<MatchDetailsComponent>;
  let httpMock: HttpTestingController;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MatchDetailsComponent],
      imports: [HttpClientTestingModule, MatSnackBarModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchDetailsComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    compiled = fixture.nativeElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('Contar goles valid', () => {
    expect(component.goalCount([2, 1, 3]) == 6).toBeTruthy();
  });

  it('Contar goles invalid', () => {
    expect(component.goalCount([2, 1, 3]) == 1).toBeFalsy();
  });

  it('Contar asistencias valid', () => {
    expect(component.assistCount([2, 1, 3]) == 6).toBeTruthy();
  });

  it('Contar asistencias invalid', () => {
    expect(component.assistCount([2, 1, 3]) == 3).toBeFalsy();
  });
});
