import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  constructor(private http: HttpClient) { }
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  apiUrl = 'https://localhost:7197/api/Members';

   getRequest(): Observable<any>;
   getRequest(id: string): Observable<any>;
   //`${this.apiUrl}/
  getRequest(id?: string): Observable<any> {
    let url:string ='';
    if (id) {
      console.log(`Fetching data for ID: ${id}`);
      url = `${this.apiUrl}/GetMember/${id}`;
    } else {
      console.log('Fetching all data');
      url = `${this.apiUrl}/GetAllMembers/`;
    }
    // Add more headers if needed
    return this.http.get(url, { headers: this.headers });
  }

  addRequest(record: any): Observable<any> {
    console.log(`Adding Record`);
    const url = `${this.apiUrl}}/Create/`;

    return this.http.post<any>(url, record, { headers: this.headers })
  }
  
  updateRequest(record: any): Observable<any> {
    console.log(`Updating Record`);
    const url = `${this.apiUrl}/Update/${record.id}`;
    
    return this.http.put(url, record, { headers: this.headers });
  }
  
  deleteRequest(id: string): Observable<any> {
    const url = `${this.apiUrl}/Delete/${id}`;
   
    return this.http.delete(url, { headers: this.headers });
  }
  
  /*
  getRequest()
  {
    const url = `${this.apiUrl}/GetAllMembers/`;
    return this.http.get<any>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Unknown error occurred';
        if (error.error instanceof ErrorEvent) {
          // Client-side error
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // Server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage); // Throw an observable error
      })
    );
  }
  */


}

