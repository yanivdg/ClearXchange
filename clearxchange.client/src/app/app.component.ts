import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild,Input } from '@angular/core';
import { MemberFormComponent } from './member-form/member-form.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  //public member: Member[] = [];
  @Input() operation: string | undefined;
  selectedTabIndex = 0;
  constructor(private http: HttpClient) {}
  /*
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
  */
  
  ngOnInit() {
    
  }
  title = 'ClearXchangeApp';
}
