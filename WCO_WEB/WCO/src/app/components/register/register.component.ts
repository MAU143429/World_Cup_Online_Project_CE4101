import { Component, OnInit } from '@angular/core';
import { Countries } from '../../interface/countries';
import countriesData from '../../../assets/scripts/countries.json';
import { CreateAccount } from 'src/app/model/create-account';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  Users: CreateAccount[] = [];
  newAccount: CreateAccount = new CreateAccount();
  age: number = 0;
  constructor(
    private service: AccountService,
    private _snackBar: MatSnackBar) {}

  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
   async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }  

  ngOnInit(): void {}

  CData: any[] = countriesData;
  

  openError(message: string) {
    this._snackBar.open(message, 'Intente de nuevo',{
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',      
    })
  };

  /**
   * Este metodo permite realizar las verificaciones del formulario de crear
   * cuenta asi como posteriormente realizar el envio de los datos.
   */
  addUser() {
    console.table(this.newAccount);
  
    this.delay(50).then(() => {
      this.service
        .getAccountByEmail(this.newAccount.email)
        .subscribe((data) => (this.Users = data));
    });

    //Verificar que esten todos los espacios requeridos
    if (!(this.newAccount.email.length > 0 && this.newAccount.password.length > 0
        && this.newAccount.name.length > 0 && this.newAccount.lastname.length > 0
        && this.newAccount.nickname.length > 0  && this.newAccount.birthdate.length > 0 
        && this.newAccount.country.length > 0)){
      this.openError("Faltan espacios requeridos para registrarse")
    }

    else{ 
      this.CalculateAge()
    }
      

    // Verificar formato y existencia de la cuenta con este corre
    
    // Faltaria revisar formato de correo

    if (this.Users.length > 0){
      this.openError("Hay una cuenta existente con este correo")
    }

    else if (!(this.newAccount.name.length < 30)){
      this.openError("Nombre excede 30 caracteres permitidos")
    }

    else if (!(this.newAccount.lastname.length < 30)){
      this.openError("Apellido excede 30 caracteres permitidos")
    }

  
    // Verificar extension y formato de contraseña
    
    else if (!(this.newAccount.password.length >= 6 && this.newAccount.password.length <= 16)){
      this.openError("Contraseña debe tener una extensión entre 6 y 16 caractéres")
    }

    // Verificar la fecha de nacimiento (mayor de 18)
    // create account no deberia devolver la edad desde la base de datos como atributo derivado?

    else if (!(this.age >= 18)){
      this.openError("Debe ser mayor de 18 años")
    }
    
    // Mandar a crear la cuenta
    else{
      this.service.createAccount(this.newAccount)
    }
  }

  CalculateAge() {

    if (this.newAccount.birthdate) {
    var timeDiff = Math.abs(Date.now() - new Date(this.newAccount.birthdate).getTime());
    this.age = Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25);
    
    console.log(this.age)
    }
  }



}

