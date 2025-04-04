import countriesData from '../../../assets/scripts/countries.json';
import { AccountService } from '../../services/account.service';
import { CreateAccount } from '../../model/create-account';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
var sha256 = require('js-sha256');

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  Users: CreateAccount[] = [];
  UsersNickname: CreateAccount[] = [];
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
   * Metodo para calcular la edad a partir de la fecha de nacimiento
   * digitado por el usuario
   */
  public CalculateAge() {
    if (this.newAccount.birthdate) {
      var timeDiff = Math.abs(
        Date.now() - new Date(this.newAccount.birthdate).getTime()
      );
      this.age = Math.floor(timeDiff / (1000 * 3600 * 24) / 365.25);

      console.log(this.age);
    }
  }

  /**
   * Método para verificar fomrato de correo utilizando estándar RFC 2822
   * @param email
   * @returns Booleano de validez de correo
   */
  public checkEmailFormat(email: string) {
    let sampleRegExMail = new RegExp(
      "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"
    );
    return sampleRegExMail.test(email);
  }

  /**
   * Método para verificar fomrato de contraseña que debe ser alfanumerica con
   * al menos un caracter alfabético y un número.
   * @param email
   * @returns Booleano de validez de contraseña
   */
  checkPasswordFormat(password: string) {
    let sampleRegExMail = new RegExp(
      '^[A-Za-z0-9]*(([A-Za-z][0-9])|([0-9][A-Za-z]))[A-Za-z0-9]*$'
    );
    return sampleRegExMail.test(password);
  }

  /**
   * Método para obtener estado de checkbox para TyC
   */
  changeStatus() {
    this.checkboxStatus = !this.checkboxStatus;
  }

  /**
   * Este metodo permite realizar las verificaciones del formulario de crear
   * cuenta asi como posteriormente realizar el envio de los datos.
   */
  addUser() {
    this.service
      .getAccountByEmail(this.newAccount.email)
      .subscribe((data) => (this.Users = data));

    this.service
      .getAccountByNickname(this.newAccount.nickname)
      .subscribe((data) => (this.UsersNickname = data));

    this.delay(50).then(() => {
      if (
        this.newAccount.email == '' ||
        this.newAccount.password == '' ||
        this.newAccount.name == '' ||
        this.newAccount.lastname == '' ||
        this.newAccount.nickname == '' ||
        this.newAccount.birthdate == '' ||
        this.newAccount.country == ''
      ) {
        this.openError(
          'Faltan espacios requeridos para registrarse',
          'Intente de nuevo'
        );
      } else {
        this.CalculateAge();

        if (!this.checkboxStatus) {
          this.openError('Debe aceptar términos y condiciones', 'Volver');
        } else if (!this.checkEmailFormat(this.newAccount.email)) {
          this.openError('Formato de correo inválido', 'Intente de nuevo');
        } else if (this.Users.length > 0) {
          this.openError(
            'Hay una cuenta existente con este correo',
            'Ingrese otra para continuar'
          );
        } else if (this.UsersNickname.length > 0) {
          this.openError(
            'Hay una cuenta existente con este nickname',
            'Ingrese otro para continuar'
          );
        } else if (!this.checkPasswordFormat(this.newAccount.password)) {
          this.openError(
            'Formato de contraseña inválido, debe ser alfanumérica',
            'Intente de nuevo'
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
          this.newAccount.password = sha256(this.newAccount.password);
          this.service
            .createAccount(this.newAccount)
            .subscribe((data) => console.log(data));
          this.openSuccess('Cuenta creada con éxito', 'Ok');
          this.router.navigate(['/login']);
        }
      }
    });
  }
}
