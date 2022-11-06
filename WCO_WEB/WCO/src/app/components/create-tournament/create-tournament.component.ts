import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { CreateTournament } from '../../model/create-tournament';
import { setOptions, localeEs } from '@mobiscroll/angular';
import { TournamentService } from '../../services/tournament.service';
import { Type } from '../../interface/type';
import { Router } from '@angular/router';
import { BracketName } from '../../interface/bracket-name';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DbTeam } from '../../model/db-team';
import { InternalService } from '..//../services/internal.service';
import { Dropdown } from '../../model/dropdown';
import { MatSnackBar } from '@angular/material/snack-bar';

export const ITEMS: Type[] = [
  { type: 'Selecciones' },
  { type: 'Equipos Locales' },
];

setOptions({
  locale: localeEs,
  themeVariant: 'light',
});

@Component({
  selector: 'app-create-tournament',
  templateUrl: './create-tournament.component.html',
  styleUrls: ['./create-tournament.component.css'],
})
export class CreateTournamentComponent implements OnInit {
  /**Instanciando variables del sistema */
  newTournament: CreateTournament = new CreateTournament();
  radioSel: any;
  radioSelected: string = '';
  Option: Type[] = ITEMS;
  allAvailableTeams: DbTeam[] = [];
  dropdownData: Dropdown[] = [];
  displayedColumns: string[] = ['name', 'action'];
  bracketSource: BracketName[] = [];
  selectedTeams: string[] = [];
  closeResult = '';

  /**
   * Este metodo permite realizar un pequeño delay
   * @param ms el tiempo del delay en ms
   */
  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  @ViewChild(MatTable, { static: true }) table: MatTable<any> | any;
  constructor(
    private modalService: NgbModal,
    public dialog: MatDialog,
    private service: TournamentService,
    private internal: InternalService,
    private router: Router,
    private _snackBar: MatSnackBar
  ) {}

  /**
   * Este metodo permite la creacion de un pop up
   * @param content es el template que contiene el contenido del popup
   */
  open(content: any) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }

  /**
   * Metodo que permite crear las acciones del boton exit del popup
   * @param reason
   * @returns
   */
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  /**
   * @brief Este metodo permite obtener el valor seleccionado en el radio button y realizar alguna acciones
   * relacionadas a la seleccion dada por el usuario
   */
  getSelecteditem() {
    this.radioSel = ITEMS.find((Item) => Item.type === this.radioSelected);
    this.newTournament.type = this.radioSelected;
    if (this.radioSelected == 'Selecciones') {
      this.service.getTeamsbyType('Selection').subscribe((data) => {
        this.allAvailableTeams = data;

        var teams: Dropdown[] = [];
        this.allAvailableTeams.forEach((value) => {
          var dropdownObject: Dropdown = { text: '', value: 0 };
          dropdownObject.text = value.name;
          dropdownObject.value = value.teId;
          teams.push(dropdownObject);
        });

        this.dropdownData = teams;
      });
    } else {
      this.service.getTeamsbyType('Local').subscribe((data) => {
        this.allAvailableTeams = data;

        var teams: Dropdown[] = [];
        this.allAvailableTeams.forEach((value) => {
          var dropdownObject: Dropdown = { text: '', value: 0 };
          dropdownObject.text = value.name;
          dropdownObject.value = value.teId;
          teams.push(dropdownObject);
        });

        this.dropdownData = teams;
      });
    }
  }

  /**
   * @brief Este metodo detecta cuando existe un cambio en el radio button
   */
  onItemChange() {
    this.getSelecteditem();
  }

  /**
   * Este metodo permite cambiar el formato de la lista de brackets
   * para que pueda ser enviado en un formato aceptado en el API
   */
  setBracketList() {
    this.bracketSource.forEach((value) => {
      this.newTournament.brackets.push(value.name);
    });
  }

  openSnackBar(message: string, message2: string) {
    this._snackBar.open(message, message2, {
      duration: 2000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: 'red-snackbar',
    });
  }

  /**
   * Este metodo permite realizar todas las verificaciones de formulario para crear torneo
   * asi como posteriormente enviar la informacion para almacenar en la DB
   */
  addTournament() {
    this.setBracketList();

    const startD = new Date(this.newTournament.startDate);
    const endD = new Date(this.newTournament.endDate);
    const today = new Date();

    if (
      this.newTournament.endDate == '' ||
      this.newTournament.name == '' ||
      this.newTournament.startDate == '' ||
      this.newTournament.teams.length == 0 ||
      this.newTournament.brackets.length == 0
    ) {
      this.openSnackBar(
        'Falta al menos uno de los espacios requeridos!',
        'Intente de nuevo'
      );
    } else if (startD > endD) {
      this.openSnackBar(
        'Error en fechas ingresadas!',
        'Fecha final incorrecta!'
      );
    } else if (today > startD || today > endD) {
      this.openSnackBar(
        'Error no se pueden generar torneos en el pasado!',
        'Ingrese una fecha correcta para continuar!'
      );
    } else if (this.newTournament.teams.length < 2) {
      this.openSnackBar(
        'Se requiere al menos dos equipos para crear un torneo!',
        'Ingrese algún otro antes de continuar!'
      );
    } else {
      this.service
        .setTournament(this.newTournament)
        .subscribe((tournament) => console.log(this.newTournament));

      this.delay(100).then(() => {
        this.router.navigate(['']);
      });
    }
  }

  ngOnInit(): void {}

  /**
   * Este metodo permite controlar las ventanas emergentes(pop up)
   * para la tabla de insertar fases del torneo
   * @param action string de la accion a realizar
   * @param obj objeto al que se le realiza la accion
   */
  openDialog(action: String, obj: any) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj,
      panelClass: 'custom-modalbox',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result.event == 'Agregar') {
        this.addRowData(result.data);
      } else if (result.event == 'Eliminar') {
        this.deleteRowData(result.data);
      }
    });
  }

  /**
   * Este metodo recibe un objeto el cual posteriormente ingresa en la tabla
   * de fases.
   * @param row_obj el objeto a ingresar
   */
  addRowData(row_obj: any) {
    this.bracketSource.push({
      name: row_obj.name,
    });
    this.table.renderRows();
  }

  /**
   * Este metodo recibe un objeto el cual posteriormente elimina de la tabla
   * @param row_obj el objeto a eliminar
   */
  deleteRowData(row_obj: any) {
    this.bracketSource = this.bracketSource.filter((value, key) => {
      return value.name != row_obj.name;
    });
  }
}
