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

  addLogin() {
    //Verificar que esten todos los espacios requeridos

    // Verificar el formato del correo

    // Verificar extension de contraseña

    // Verificar es que el correo tenga una cuenta en WCO

    this.service
      .getAccountByEmail(this.newLogin.email)
      .subscribe((data) => (this.Users = data));

    //Verificar las credenciales
  }
}
