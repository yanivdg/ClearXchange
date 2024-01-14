import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MemberFormModule } from './member-form/member-form.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { NumericInputDirective } from './numeric-input.directive'; // Import your directive
@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatTabsModule,
    RouterOutlet,
    FormsModule,
    MemberFormModule,
    MatButtonModule,
    ReactiveFormsModule
    // other modules
  ],
  declarations: [AppComponent, NumericInputDirective],
  bootstrap: [AppComponent],
})
export class AppModule { }
