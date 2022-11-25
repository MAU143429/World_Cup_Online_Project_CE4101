import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGroupsComponent } from '../../src/app/components/create-groups/create-groups.component';

describe('CreateGroupsComponent', () => {
  let component: CreateGroupsComponent;
  let fixture: ComponentFixture<CreateGroupsComponent>;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateGroupsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateGroupsComponent);
    component = fixture.componentInstance;
    compiled = fixture.nativeElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  test('debe de hace match con el snapshot', () => {
    expect(compiled.innerHTML).toMatchSnapshot();
  });
});
