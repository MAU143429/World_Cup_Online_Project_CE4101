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
  dropdownData: String[] = [];
  displayedColumns: string[] = ['name', 'action'];
  bracketSource: BracketName[] = [];
  selectedTeams: string[] = [];

  async delay(ms: number) {
    await new Promise<void>((resolve) => setTimeout(() => resolve(), ms)).then(
      () => console.log('fired')
    );
  }

  @ViewChild(MatTable, { static: true }) table: MatTable<any> | any;
  constructor(
    public dialog: MatDialog,
    private service: TournamentService,
    private router: Router
  ) {}

  /**
   * @brief Este metodo permite obtener el valor seleccionado en el radio button y realizar alguna acciones
   * relacionadas a la seleccion dada por el usuario
   */
  getSelecteditem() {
    this.radioSel = ITEMS.find((Item) => Item.type === this.radioSelected);
    this.newTournament.type = this.radioSelected;
    if (this.radioSelected == 'Selecciones') {
      this.service.getTeams().subscribe((data) => (this.dropdownData = data));
    } else {
      this.service.getCFTeams().subscribe((data) => (this.dropdownData = data));
    }
  }

  /**
   * @brief Este metodo detecta cuando existe un cambio en el radio button
   */
  onItemChange() {
    this.getSelecteditem();
  }

  setBracketList() {
    this.bracketSource.forEach((value) => {
      this.newTournament.brackets.push(value.name);
    });
  }

  addTournament() {
    this.setBracketList();

    this.service
      .setTournament(this.newTournament)
      .subscribe((tournament) => console.log(this.newTournament));

    this.delay(100).then(() => {
      this.router.navigate(['']);
    });
  }

  ngOnInit(): void {}

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

  addRowData(row_obj: any) {
    this.bracketSource.push({
      name: row_obj.name,
    });
    this.table.renderRows();
  }

  deleteRowData(row_obj: any) {
    this.bracketSource = this.bracketSource.filter((value, key) => {
      return value.name != row_obj.name;
    });
  }
}
