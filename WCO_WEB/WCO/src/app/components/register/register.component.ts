import { Component, OnInit } from '@angular/core';
import { Countries } from '../../interface/countries';
import countriesData from '../../../assets/scripts/countries.json';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  CData: any[] = countriesData;
}
