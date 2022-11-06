import { Component, OnInit } from '@angular/core';
import { NgbDateNativeAdapter } from '@ng-bootstrap/ng-bootstrap';
import { Login } from 'src/app/model/login';
import { CreateAccount } from '../../model/create-account';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  Users: CreateAccount[] = [];
  newLogin: Login = new Login();

  constructor(private service: AccountService) {}

  ngOnInit(): void {}

  /**
   * Este metodo permite hacer todas las verificaciones del inicio de sesion
   * asi como la comprobacion de contraseñas y posteriormente el paso a WCO
   */
  addLogin() {
    //Verificar que esten todos los espacios requeridos
    // Verificar el formato del correo
    // Verificar extension de contraseña
    // Verificar es que el correo tenga una cuenta en WCO
    //Verificar las credenciales
  }
}
