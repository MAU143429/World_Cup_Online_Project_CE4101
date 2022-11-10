import {
  Directive,
  Input,
  OnInit,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { AccountService } from '../services/account.service';

@Directive({
  selector: '[appRole]',
})
export class RoleDirective implements OnInit {
  private currentUser: any;
  private permissions: any;

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private service: AccountService
  ) {}

  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  ngOnInit(): void {
    this.service.getRole().subscribe((role) => {
      if (role) {
        localStorage.setItem('scopes', 'admin');
      }
    });

    this.delay(50).then(() => {
      this.updateView();
    });
  }

  @Input()
  set appRole(val: Array<string>) {
    this.permissions = val;
    this.updateView();
  }

  private updateView(): void {
    this.viewContainer.clear();
    if (localStorage.getItem('scope') === 'admin') {
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }
}
