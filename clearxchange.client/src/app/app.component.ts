import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Directionality } from '@angular/cdk/bidi';

export enum Gender {
  Male,
  Female,
  Other
}
interface Member {
  Id: string;
  Name: string;
  Email: string;
  dateofbirth: Date;
  gender: Gender;
  phone: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  public member: Member[] = [];
  selectedTabIndex = 0; 
  constructor(private http: HttpClient) {}
  addTabActive = false;
  updateTabActive = false;
  deleteTabActive = false;
  displayTabActive = false;

  setActiveTab(tab: 'add' | 'update' | 'delete' | 'display'): void {
    this.addTabActive = tab === 'add';
    this.updateTabActive = tab === 'update';
    this.deleteTabActive = tab === 'delete';
    this.displayTabActive = tab === 'display';
  }
  ngOnInit() {
    
  }
  title = 'ClearXchangeApp';
}
