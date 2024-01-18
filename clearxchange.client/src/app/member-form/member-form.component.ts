import { Component, OnInit, ElementRef,ViewChild,Input} from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MemberObj,Gender } from '../models/member.model';
import { DataService } from '../services/data.service';
import {  Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { format } from 'date-fns-tz';


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
  constructor(private fb: FormBuilder, private dataService: DataService,private datePipe: DatePipe,private snackBar: MatSnackBar ) { }


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
  
  openSnackbar(message:string) {
    this.snackBar.open(message, 'סגור', {
      duration: 2000, // Duration in milliseconds
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }
  //
  onSearch()
  {
    const control = this.memberForm.get('Id');
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
      },
      (error) => {
        console.error('Error:', error);
        this.openSnackbar('לא נמצא חבר');
        
      }
      
    );
    if(this.memberObj!=null)
    {
      this.memberForm.patchValue(this.memberObj);
    }
    }
  }
  //
  OnDeleteRequest()
  {
    const control = this.memberForm.get('Id');
    const Id = control?.value;

      this.dataService.deleteRequest(Id).pipe(
        switchMap((response: any) => {
          // Handle the response here
          return new Observable(observer => {
            observer.next(response); // Pass the modified response downstream
            observer.complete(); // Complete the Observable
          });
        })
      ).subscribe(
        (response: any) => {
          // Handle the response or perform subsequent operations if needed
          //this.elementsRetrieved.emit(response);
        },
        (error) => {
          console.error('Error:', error);
          this.openSnackbar('תקלה במחיקת החבר מהמערכת נא פנה לאחראי מערכת');
        }
      );
      this.openSnackbar('החבר נמחק בהצלחה');
  }
  OnUpdateRequest()
  {
    this.memberObj = this.memberForm.value;
    //this.memberObj.DateOfBirth = this.formatDate(this.memberObj.dateOfBirth); 
 
      this.dataService.updateRequest(this.memberObj).pipe(
        switchMap((response: any) => {
          // Handle the response here
          return new Observable(observer => {
            observer.next(response); // Pass the modified response downstream
            observer.complete(); // Complete the Observable
          });
        })
      ).subscribe(
        (response: any) => {
          // Handle the response or perform subsequent operations if needed
          //this.elementsRetrieved.emit(response);
          this.openSnackbar('החבר עודכן בהצלחה');
        },
        (error) => {
          console.error('Error:', error);
          this.openSnackbar('תקלה בעדכון החבר במערכת נא פנה לאחראי מערכת');
        }
      );
  }
  OnAddRequest()
  {
    this.memberObj = this.memberForm.value;
    const control = this.memberForm.get('DateOfBirth');
    const DOB = control?.value;
    // Convert and format with date-fns-tz
    this.memberObj.DateOfBirth = format(DOB, "yyyy-MM-dd'T'HH:mm:ss.000'Z'", { timeZone: 'Asia/Jerusalem' });
    //this.memberObj.DateOfBirth = this.formatDate(this.memberObj.dateOfBirth); 
      this.dataService.addRequest(this.memberObj ).pipe(
        switchMap((response: any) => {
          // Handle the response here
          return new Observable(observer => {
            observer.next(response); // Pass the modified response downstream
            observer.complete(); // Complete the Observable
            this.openSnackbar('החבר הוסף בהצלחה למערכת');
          });
        })
      ).subscribe(
        (response: any) => {
          // Handle the response or perform subsequent operations if needed
          //this.elementsRetrieved.emit(response);
        },
        (error) => {
          console.error('Error:', error);
          this.openSnackbar('תקלה בהוספת החבר במערכת נא פנה לאחראי מערכת');
        }
      );
  }

  onSubmit() {
    if (this.memberForm?.valid) {
      // Process the form data
      //this.memberObj = this.memberForm.value;
      //this.memberObj.dateofbirth = this.formatDate(this.memberObj.dateofbirth);
      //
      switch (this.operation) {
        case 'add':
          console.log('Performing add operation');
          this.OnAddRequest();
          break;
      
        case 'update':
          console.log('Performing update operation');
          this.OnUpdateRequest();
          break;
      
        case 'delete':
          console.log('Performing delete operation');
          this.OnDeleteRequest();
          break;
      
        default:
          console.log('Invalid operation');
      }

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
