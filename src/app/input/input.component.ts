import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {
  calculatorForm: FormGroup | undefined;
  constructor() { }

  ngOnInit(): void {
    this.calculatorForm = new FormGroup({
      issueDate: new FormControl(''),
      returnDate: new FormControl(''),
      countries:new FormControl('')
    });
  }

}
