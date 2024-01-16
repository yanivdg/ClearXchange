import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisplayFormComponent } from './display-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
/*
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';
*/

@NgModule({
  declarations: [DisplayFormComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    /*
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatButtonModule
    */
],
  providers:[],
  exports: [DisplayFormComponent]
},)

export class DisplayFormModule {

}
