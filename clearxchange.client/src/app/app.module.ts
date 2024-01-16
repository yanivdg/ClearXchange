import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MemberFormModule } from './member-form/member-form.module';
import { DisplayFormModule } from './display-form/display-form.module';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatTabsModule,
    RouterOutlet,
    FormsModule,
    MatButtonModule,
    ReactiveFormsModule,
    MemberFormModule,
    DisplayFormModule
    // other modules
  ],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
})

export class AppModule {
  
 }
