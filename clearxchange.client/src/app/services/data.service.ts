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

   getRequest(): Observable<any>
   {
      let url:string ='';
      console.log('Fetching all data');
      url = `${this.apiUrl}/GetAllMembers/`;
      // Add more headers if needed
      return this.http.get(url, { headers: this.headers });
   }


   getRequestbyVal(id: string): Observable<any>
   {
    let url:string ='';
    if (id!= '' && id!=undefined) {
      console.log(`Fetching data for ID: ${id}`);
      url = `${this.apiUrl}/GetMember/${id}`;
    } 
  return this.http.get(url, { headers: this.headers });
   }


  addRequest(record: any): Observable<any> {
    let url:string ='';
    if(record!=undefined)
    {
    console.log(`Adding Record`);
    url = `${this.apiUrl}/Create/`;
    }
    return this.http.post<any>(url, record, { headers: this.headers })
  }
  
  updateRequest(record: any): Observable<any> {
    let url:string ='';
    if (record.Id!= '' && record.Id!=undefined) 
    { 
      console.log(`Updating Record ${record.Id}`);
      url = `${this.apiUrl}/Update/${record.Id}`;
    }
    return this.http.put(url, record, { headers: this.headers });
  }
  
  deleteRequest(id: string): Observable<any> {
    let url:string ='';
    if (id!= '' && id!=undefined)  {
      console.log(`Updating Record ${id}`);
      url = `${this.apiUrl}/Delete/${id}`;
    }
    return this.http.delete(url, { headers: this.headers });
  }
}

