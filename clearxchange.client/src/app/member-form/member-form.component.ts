import { Component, OnInit, ElementRef,ViewChild,Input} from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MemberObj,Gender } from '../models/member.model';
import { DataService } from '../services/data.service';
import {  Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-member-form',
  templateUrl: './member-form.component.html',
  styleUrl: './member-form.component.css'
})
export class MemberFormComponent implements OnInit {
  @Input() operation: string | undefined;
  memberObj: MemberObj={
    Id: '',
    Name: '',
    Email: '',
    DateOfBirth: '',
    Gender: 0,
    Phone: ''
  };
  
  memberForm!: FormGroup;
  @ViewChild('dob') myFormControlElementRef!: ElementRef;
  constructor(private fb: FormBuilder, private dataService: DataService,private datePipe: DatePipe ) { }

  ngOnInit() {
    this.memberForm = this.fb.group({
      Id: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]{1,9}$/)]),  
      Name: new FormControl('', Validators.required),
      DateOfBirth: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Phone: new FormControl('', [Validators.pattern(/^[0-9]+$/)]),
      Gender:new FormControl('')
    });
    
  }

  onKeyPress(event: KeyboardEvent, inputElement: HTMLInputElement,field:string): void {
    // No need to check for nativeElement in this case
    // HTMLInputElement is directly used
    if (event.target !== inputElement) {
      return;
    }

    if (event.keyCode < 33 || event.keyCode == 127) { return; }
    console.log('Keydown event:', event);
    // Apply specific limitations based on the field
    if (field === 'N9' || field === 'N') {
      // Limit to digits
      if (!(event.key >= '0' && event.key <= '9'))
      {
        event.preventDefault();
      }
    } else if (field === '@') {
      // Validate email format

      if (event.key === '@' && inputElement.value.includes('@')) {
        event.preventDefault();
      }
    } else if (field === 'D') {
      const enteredChar = event.key;

      // Regular expression for MM/DD/YYYY format
      const datePattern = /^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$/;

      // Check if the entered character is a digit or a valid separator
      if (!/\d|[/]/.test(enteredChar)) {
        event.preventDefault();
        return;
      }

      // Check the date pattern after adding the entered character
      const updatedValue = inputElement.value + enteredChar;
      if (!datePattern.test(updatedValue)) {
        event.preventDefault();
      }
    }

  }

  formatDate(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-ddTHH:mm:ss.fff');
  }
  
  //
  onSearch()
  {
    const control = this.memberForm.get('id');
    const value = control?.value;
    if(value!='')
    {
    this.dataService.getRequest(value).pipe(
      switchMap((response: any) => {
        // Handle the response here
        this.memberObj = response;
        return new Observable(observer => {
          observer.next(response.body); // Pass the modified response downstream
          observer.complete(); // Complete the Observable
        });
      })
    ).subscribe(
      (response: any) => {
        // Handle the response or perform subsequent operations if needed
        //this.elementsRetrieved.emit(response);
        this.memberObj = response;

        this.memberForm = new FormGroup({
          id: new FormControl(this.memberObj.Id),
          name: new FormControl(this.memberObj.Name),
          dateOfBirth: new FormControl(this.memberObj.DateOfBirth),
          email: new FormControl(this.memberObj.Email),
          phone: new FormControl(this.memberObj.Phone),
          gender: new FormControl(this.memberObj.Gender)
        });
        
      },
      (error) => {
        console.error('Error:', error);
        // Handle errors
      }
    );
    }
  }
  //
  onSubmit() {
    if (this.memberForm?.valid) {
      // Process the form data
      //this.memberObj = this.memberForm.value;
      //this.memberObj.dateofbirth = this.formatDate(this.memberObj.dateofbirth);
      //
      this.memberObj = {
      "Id": "128655570",
      "Name": "string",
      "Email": "user@example.com",
      "DateOfBirth": "2008-01-15T22:27:49.373Z",
      "Gender": Gender.Female,
      "Phone": "499997"
      }

      const record = {
          "id": "073810194",
          "name": "string",
          "email": "user@example.com",
          "dateOfBirth": "2018-02-31",
          "gender": 0,
          "phone": "347305344986734155724807487383991141904967933348"
      }

      this.dataService.updateRequest(record).pipe(
        switchMap((response: any) => {
          // Handle the response here
          alert(response);
          return new Observable(observer => {
            observer.next(response); // Pass the modified response downstream
            observer.complete(); // Complete the Observable
          });
        })
      ).subscribe(
        (response: any) => {
          // Handle the response or perform subsequent operations if needed
          //this.elementsRetrieved.emit(response);
          alert(response);
        },
        (error) => {
          alert('Error:' + error.message);
          console.error('Error:', error);
        }
      );
      console.log(this.memberForm.value);
    } else {
      // Mark form fields as touched to display validation messages
      this.markFormFieldsAsTouched();
    }
  }
  
  isFieldInvalid(field: string) { 
    const control = this.memberForm.get(field);
    return control ? control.invalid && control.touched : false;
  }

  markFormFieldsAsTouched() {
    
    Object.keys(this.memberForm.controls).forEach(field => {
      const control = this.memberForm.get(field);
      if (control) {
        control.markAsTouched({ onlySelf: true });
      }
    });
    
  }
 
}
