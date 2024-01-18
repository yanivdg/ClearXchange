import { Component, OnInit, ElementRef,ViewChild,Input,EventEmitter} from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MemberObj,Gender } from '../models/member.model';
import { DataService } from '../services/data.service';
import {  Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-display-form',
  templateUrl: './display-form.component.html',
  styleUrl: './display-form.component.css'
})
export class DisplayFormComponent implements OnInit{
  constructor(private dataService:DataService){}
  public eventEmitter = new EventEmitter<void>();
  elementsRetrieved: EventEmitter<string> = new EventEmitter<string>();
  //members:MemberObj[] = [];
  members:any;
  display:boolean=false;
  DisplayMembers():void{
    this.dataService.getRequest().pipe(
      switchMap((response: any) => {
        // Handle the response here
        this.members = response;
        return new Observable(observer => {
          observer.next(response); // Pass the modified response downstream
          observer.complete(); // Complete the Observable
        });
      })
    ).subscribe(
      (response: any) => {
        // Handle the response or perform subsequent operations if needed
        this.elementsRetrieved.emit(response);
        this.members = response;
      },
      (error) => {
        console.error('Error:', error);
        // Handle errors
      }
    );
    this.display = true;
  }
  ngOnInit(): void {

  }

 getFieldNames(obj: any): string[] {
    return Object.keys(obj);
  }

  getHeaderRow(): string[] {
    // Use the keys of the first object as headers
    return this.getFieldNames(this.members[0]);
  }
}
