import { Component, OnInit } from '@angular/core';
import { NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap';
import { Login } from 'src/app/model/login';
import { CreateAccount } from '../../model/create-account';
import { AccountService } from '../../services/account.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  Users: CreateAccount[] = [];
  newLogin: Login = new Login();

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
  openError(message: string) {
    this._snackBar.open(message, 'Intente de nuevo',{
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',      
    })
  };

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

    this.delay(50).then(() => {
      this.service
        .getAccountByEmail(this.newLogin.email)
        .subscribe((data) => (this.Users = data));
    });

    //Verificar que esten todos los espacios requeridos
    if ((this.newLogin.email == "" ||
         this.newLogin.password == "")){
      this.openError("Faltan espacios requeridos para iniciar sesión")
    }
    // Verificar es que el correo tenga una cuenta en WCO (Si verifico esto, y despues digo que credenciales estan mal, entonces es obvio que la contraseña es la que está mal)
    else if (!(this.Users.length > 0)){
      this.openError("Cuenta con correo no registrado")
    }
    // Verificar las credenciales
    else if (!(this.Users[0].password == this.newLogin.password)){
      this.openError("Credenciales incorrectos")
    } else {
      this.openSuccess('Cuenta creada con éxito', 'Ok');
      this.router.navigate(['/home']);
    }
  }
}
