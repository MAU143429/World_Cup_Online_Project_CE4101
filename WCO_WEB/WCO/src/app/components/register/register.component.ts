import { Component, OnInit } from '@angular/core';
import { Countries } from '../../interface/countries';
import countriesData from '../../../assets/scripts/countries.json';
import { CreateAccount } from 'src/app/model/create-account';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  newAccount: CreateAccount = new CreateAccount();
  constructor() {}

  ngOnInit(): void {}

  CData: any[] = countriesData;

  addUser() {
    console.table(this.newAccount);

    //Verificar que esten todos los espacios requeridos

    // Verificar formato y existencia de la cuenta con este corre

    // Verificar extension y formato de contraseña

    // Verificar la fecha de nacimiento (mayor de 18)

    // Verificar que se hayan aceptado los terminos

    // Mandar a crear la cuenta
  }
}
