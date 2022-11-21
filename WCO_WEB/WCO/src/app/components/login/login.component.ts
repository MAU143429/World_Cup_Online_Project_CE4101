import { AccountService } from '../../services/account.service';
import { CreateAccount } from '../../model/create-account';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/model/login';
import { Router } from '@angular/router';
var sha256 = require('js-sha256');

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  Users: CreateAccount[] = [];
  newLogin: Login = new Login();
  credentials: Login = new Login();

  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  constructor(
    private service: AccountService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {}

  /**
   * Metodo para mostrar alerta de error por 2 segundos
   * @param message1 Mensaje de error
   * @param message2 Mensaje para cerrar alerta
   */
  openError(message: string, message2: string) {
    this._snackBar.open(message, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',
    });
  }

  /**
   * Metodo para mostrar alerta de exito por 2 segundos
   * @param message1 Mensaje de exito
   * @param message2 Mensaje para cerrar alerta
   */
  openSuccess(message1: string, message2: string) {
    this._snackBar.open(message1, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'green-snackbar',
    });
  }

  /**
   * Este metodo permite hacer todas las verificaciones del inicio de sesion
   * asi como la comprobacion de contraseñas y posteriormente el paso a WCO
   */
  addLogin() {
    this.service
      .getAccountByEmail(this.newLogin.email)
      .subscribe((data) => (this.Users = data));

    this.delay(50).then(() => {
      //Verificar que esten todos los espacios requeridos
      if (this.newLogin.email == '' || this.newLogin.password == '') {
        this.openError(
          'Faltan espacios requeridos para iniciar sesión',
          'Intente de nuevo'
        );
      }
      // Verificar es que el correo tenga una cuenta en WCO (Si verifico esto, y despues digo que credenciales estan mal, entonces es obvio que la contraseña es la que está mal)
      else if (!(this.Users.length > 0)) {
        this.openError('Cuenta con correo no registrado', 'Intente de nuevo');
      } else {
        this.credentials.email = this.newLogin.email;
        this.credentials.password = sha256(this.newLogin.password);
        var login = false;
        this.service
          .newLogin(this.credentials)
          .subscribe((data) => (login = data));
        console.log(login);
        this.delay(50).then(() => {
          if (login) {
            localStorage.setItem('email', this.newLogin.email);
            localStorage.setItem('nickname', this.Users[0].nickname);
            this.service.getRole().subscribe((role) => {
              if (role) {
                localStorage.setItem('scope', 'admin');
              } else {
                localStorage.setItem('scope', 'user');
              }
            });
            this.openSuccess('Inicio de sesión exitoso', 'Ok');
            this.delay(50).then(() => {
              this.router.navigate(['/home']);
            });
          } else {
            this.openError('Credenciales incorrectas', 'Intente de nuevo');
          }
        });
      }
    });
  }
}
