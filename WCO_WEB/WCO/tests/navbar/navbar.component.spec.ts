import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarComponent } from '../../src/app/components/navbar/navbar.component';

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    compiled = fixture.nativeElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  test("Se verifican routerlinks", () => {
    const logoElm: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#logoElement');
    expect(logoElm.getAttribute('routerLink')).toEqual('/home')

    const scoreElm: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#scoreElement');
    expect(scoreElm.getAttribute('routerLink')).toEqual('/scores')

    const groupElm: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#groupElement');
    expect(groupElm.getAttribute('routerLink')).toEqual('/groups')

    const logoutElm: HTMLInputElement = fixture.debugElement.nativeElement.querySelector('#logoutElement');
    expect(logoutElm.getAttribute('routerLink')).toEqual('/login')
  })
});
