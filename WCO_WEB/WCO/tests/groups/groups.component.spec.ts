import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsComponent } from '../../src/app/components/groups/groups.component';

describe('GroupsComponent', () => {
  let component: GroupsComponent;
  let fixture: ComponentFixture<GroupsComponent>;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GroupsComponent);
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
