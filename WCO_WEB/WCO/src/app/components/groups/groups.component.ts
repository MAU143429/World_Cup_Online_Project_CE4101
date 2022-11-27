import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { JoinGroup } from 'src/app/model/join-group';
import { AccountService } from 'src/app/services/account.service';
import { Group } from '../../interface/group';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css'],
})
export class GroupsComponent implements OnInit {
  status = true;
  groupCode = '';
  groupList: any;
  newMember: JoinGroup = new JoinGroup();
  groupsData: Group[] = [];
  constructor(
    private service: AccountService,
    private _snackBar: MatSnackBar
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

  ngOnInit(): void {
    this.updateGroups();
  }

  joinGroup() {
    this.verifyGroupData();
    // Verificar que se lleno el campo
    this.service
      .getGroupByID(this.groupCode)
      .subscribe((data) => (this.groupList = data));

    this.delay(50).then(() => {
      if (this.groupCode == '') {
        this.openError(
          'Falta al menos uno de los espacios requeridos!',
          'Intente de nuevo'
        );
      } else if (this.groupList.length == 0) {
        // Verificar que el grupo exista
        this.openError(
          'Error en el código ingresado',
          'No hay ningún grupo asociado a este código'
        );
      } else if (!this.status) {
        // Verificar que el usuario no este en otro grupo del mismo torneo
        this.openError(
          'Error al unirse al grupo',
          'Usted ya se encuentra en otro grupo del mismo torneo'
        );
      } else {
        this.newMember.acc_nick = localStorage.getItem('nickname');
        this.newMember.acc_email = localStorage.getItem('email');
        this.newMember.gId = this.groupCode;
        // Agregarlo al grupo
        this.service
          .joinGroup(this.newMember)
          .subscribe((data) => console.log(data));
        this.delay(50).then(() => {
          this.updateGroups();
        });
      }
    });
  }

  verifyGroupData() {
    this.groupsData.forEach((value: any) => {
      if (value.tId == this.groupCode.substring(0, 6)) {
        this.status = false;
      }
    });
  }

  updateGroups() {
    this.service
      .getUserGroups(
        localStorage.getItem('nickname'),
        localStorage.getItem('email')
      )
      .subscribe((data) => (this.groupsData = data));
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

  showGroup(gId: any) {
    localStorage.setItem('gId', gId);
  }
}
