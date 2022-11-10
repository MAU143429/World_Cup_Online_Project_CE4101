import { Component, OnInit } from '@angular/core';
import { Countries } from '../../interface/countries';
import countriesData from '../../../assets/scripts/countries.json';
import { CreateAccount } from 'src/app/model/create-account';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  Users: CreateAccount[] = [];
  newAccount: CreateAccount = new CreateAccount();
  age: number = 0;
  checkboxStatus: Boolean = false;
  constructor(
    private service: AccountService,
    private _snackBar: MatSnackBar,
    private router: Router
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

  ngOnInit(): void {}

  CData: any[] = countriesData;

  openError(message: string, message2: string) {
    this._snackBar.open(message, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',
    });
  }

  openSuccess(message1: string, message2: string) {
    this._snackBar.open(message1, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'green-snackbar',
    });
  }

  CalculateAge() {
    if (this.newAccount.birthdate) {
      var timeDiff = Math.abs(
        Date.now() - new Date(this.newAccount.birthdate).getTime()
      );
      this.age = Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25);

      console.log(this.age);
    }
  }

  changeStatus() {
    this.checkboxStatus = !this.checkboxStatus;
  }

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
    if (
      this.newAccount.email == '' ||
      this.newAccount.password == '' ||
      this.newAccount.name == '' ||
      this.newAccount.lastname == '' ||
      this.newAccount.nickname == '' ||
      this.newAccount.birthdate == '' ||
      this.newAccount.country == '' ||
      this.checkboxStatus == false
    ) {
      this.openError(
        'Faltan espacios requeridos para registrarse',
        'Intente de nuevo'
      );
    } else {
      this.CalculateAge();
      if (this.Users.length > 0) {
        this.openError(
          'Hay una cuenta existente con este correo',
          'Ingrese otra para continuar'
        );
      } else if (
        !(
          this.newAccount.password.length >= 6 &&
          this.newAccount.password.length <= 8
        )
      ) {
        this.openError(
          'La contraseña debe tener una extensión entre 6 y 8 caractéres',
          'Intente de nuevo'
        );
      } else if (!(this.age >= 18)) {
        this.openError('Debe ser mayor de 18 años', 'Intente de nuevo');
      } else {
        this.service.createAccount(this.newAccount);
        this.openSuccess('Cuenta creada con éxito', 'Ok');
        this.router.navigate(['/login']);
      }
    }
  }
}
