import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CreateGroup } from 'src/app/model/create-group';
import { TournamentDropdown } from 'src/app/model/tournament-dropdown';
import { AccountService } from 'src/app/services/account.service';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-create-groups',
  templateUrl: './create-groups.component.html',
  styleUrls: ['./create-groups.component.css'],
})
export class CreateGroupsComponent implements OnInit {
  status: any;
  allTournaments: TournamentDropdown[] = [];
  newGroup: CreateGroup = new CreateGroup();

  constructor(
    private service: TournamentService,
    private service1: AccountService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.service.getTournaments().subscribe((data) => {
      var tournaments: TournamentDropdown[] = [];
      data.forEach((value: any) => {
        var dropdownObject: TournamentDropdown = { text: '', value: '' };
        dropdownObject.text = value.name + ' ' + value.toId;
        dropdownObject.value = value.toId;
        tournaments.push(dropdownObject);
      });

      this.allTournaments = tournaments;
    });
  }

  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

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

  addGroup() {
    this.newGroup.acc_nick = localStorage.getItem('nickname');
    this.newGroup.acc_email = localStorage.getItem('email');
    this.service1
      .getIsInGroup(
        this.newGroup.acc_email,
        this.newGroup.acc_nick,
        this.newGroup.tId
      )
      .subscribe((data) => (this.status = data));

    // Verificar que los campos requeridos esten llenos
    this.delay(50).then(() => {
      console.log(this.status);
      if (this.newGroup.name == '' || this.newGroup.tId == '') {
        this.openError(
          'Falta al menos uno de los espacios requeridos!',
          'Intente de nuevo'
        );
      } else if (this.status) {
        this.openError(
          'Error al crear grupo',
          'El usuario ya se encuentra en otro grupo de este torneo'
        );
      } else {
        // Sino esta lo crea
        this.service1
          .newGroup(this.newGroup)
          .subscribe((data) => console.log(data));
        this.delay(50).then(() => {
          this.router.navigate(['/groups']);
        });
        this.openSuccess('Grupo creado con éxito', 'Ok');
      }
    });
  }
}
