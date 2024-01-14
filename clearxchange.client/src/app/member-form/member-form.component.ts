import { Component, OnInit, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
@Component({
  selector: 'app-member-form',
  templateUrl: './member-form.component.html',
  styleUrl: './member-form.component.css'
})
export class MemberFormComponent implements OnInit {

  memberForm!: FormGroup;
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.memberForm = this.fb.group({
      id: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]{1,9}$/)]),
      name: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      dob: new FormControl('', Validators.required),
      phone: new FormControl('', [Validators.pattern(/^[0-9]+$/)])
    });
  }

  onKeyPress(event: KeyboardEvent, inputElement: HTMLInputElement,field:string): void {
    // No need to check for nativeElement in this case
    // HTMLInputElement is directly used
    if (event.target !== inputElement) {
      return;
    }

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
      // Add specific validation for the date of birth if needed
      // For example, limit the length or format
    }
    // Add more conditions for other fields as needed
  }

  onSubmit() {
    if (this.memberForm?.valid) {
      // Process the form data
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
