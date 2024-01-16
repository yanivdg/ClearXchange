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
  selectedTabIndex = 0;
  constructor(private http: HttpClient) {}
  
  ngOnInit() {
    
  }
  title = 'ClearXchangeApp';
}
