import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateTournamentComponent } from './create-tournament.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

describe('CreateTournamentComponent', () => {
  let component: CreateTournamentComponent;
  let fixture: ComponentFixture<CreateTournamentComponent>;
  let compiled: HTMLElement;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTournamentComponent ],
      imports: [ HttpClientTestingModule, MatSnackBarModule ],
    })
    .compileComponents();
    
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTournamentComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject( HttpTestingController );
    compiled = fixture.nativeElement;
    fixture.detectChanges();

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


});
