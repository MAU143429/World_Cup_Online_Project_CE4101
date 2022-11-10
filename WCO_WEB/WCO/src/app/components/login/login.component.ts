import { Component, OnInit } from '@angular/core';
import { NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap';
import { Login } from 'src/app/model/login';
import { CreateAccount } from '../../model/create-account';
import { AccountService } from '../../services/account.service';
import {MatSnackBar} from '@angular/material/snack-bar';


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
    private _snackBar: MatSnackBar
    ) {}

  ngOnInit(): void {}

  openError(message: string) {
    this._snackBar.open(message, 'Intente de nuevo',{
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',      
    })
  };


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
    if (!(this.newLogin.email.length > 0 && this.newLogin.password.length > 0)){
      this.openError("Faltan espacios requeridos para iniciar sesión")
    }
    // Verificar es que el correo tenga una cuenta en WCO (Si verifico esto, y despues digo que credenciales estan mal, entonces es obvio que la contraseña es la que está mal)
    if (!(this.Users.length > 0)){
      this.openError("Cuenta con correo no registrado")
    }
    // Verificar las credenciales
    if (!(this.Users[0].password == this.newLogin.password)){
      this.openError("Credenciales incorrectos")
    }
  }
}
