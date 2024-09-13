import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; // <-- Import FormsModule
import { HttpClientModule } from '@angular/common/http';

// Define the Quote interface
export class Quote {
  insurersName: string = ""; // The name of the insurance provider
  costPerMonth: number =0; // The cost per month for the quote
  lengthOfPolicy: number = 0;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  public firstName: string = '';
  public lastName: string = '';
  public dob: string = '';
  public email: string = '';
  public phone: string = '';
  public error: string = '';
  public returnedQuote: Quote = new Quote; // Store the returned Quote
  public onSuccess: boolean = false;  // Boolean to show/hide the card component

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  onSubmit(event: Event) {
    const formData = {
      firstName: this.firstName,
      lastName: this.lastName,
      dob: this.dob,
      email: this.email,
      phone: this.phone,
    };
    var isValid = true;
    // First Name validation

     if (this.firstName.trim() === "") {
         this.error  = "First name is required.";
         isValid = false;
     }
     // Last Name validation

     if (this.lastName.trim() === "") {
         isValid = false;
     }

     // Date of Birth validation

     if (this.dob === "") {
        this.error = "Date of birth is required.";
         isValid = false;
     }
     // Email validation

     const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
     if (!emailPattern.test(this.email)) {
         this.error = "Please enter a valid email address.";
         isValid = false;
     }

     // Phone Number validation

     const phonePattern = /^[0-9]{10}$/;
     if (!phonePattern.test(this.phone)) {
         this.error = "Please enter a 10-digit phone number.";
         isValid = false;
     }
     if (!isValid) {
         event.preventDefault();
         console.log('error') // Prevent form submission if validation fails
     }
     else{
      this.http.post<Quote>('/api/submit-quote', formData).subscribe(
        (response: Quote) => {
          // Handle the response, which is a Quote object
          this.returnedQuote = response;
          console.log('Returned Quote success');
        },
        (error) => {
          // Handle the error
          console.error('Error submitting the form:', error);
          this.error = 'An error occurred while submitting the form. Please try again.';
        }
      );
     }
    this.http.post<Quote>('http://localhost:7075/Quote/submit-quote', formData).subscribe(
      (response: Quote) => {
        // Handle the response, which is a Quote object
        this.returnedQuote = response;
        console.log('Returned Quote success');
        this.onSuccess = true; // Set to true when the request is successful
      },

      (error) => {
        // Handle the error
        console.error('Error submitting the form:', error);
        this.error =
          'An error occurred while submitting the form. Please try again.';
      }
    );
  }

  title = 'activequote.client';
}
