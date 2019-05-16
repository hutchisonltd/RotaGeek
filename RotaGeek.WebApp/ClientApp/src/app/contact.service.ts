import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map, catchError, tap } from 'rxjs/operators';
import { ContactDTO } from '../models/contact';

export interface Contact {
  id: number;
  name: string;
  emailaddress: string;
  message: string;
}

@Injectable()
export class ContactService {
  private headers: HttpHeaders;
  private accessPointUrl: string = 'https://localhost:44355/api/contact';

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    this.get();
  }

  public get() {
    return this.http.get<Contact[]>(this.accessPointUrl, { headers: this.headers });
  }

  public getById(id) {
    return this.http.get<Contact>(this.accessPointUrl + '/' + id, { headers: this.headers });
  }

  createContact(name: string, email: string, message: string): Observable<Contact> {

    const headers = new HttpHeaders().set('content-type', 'application/json');
    var body = {
      Id: 0, Name: name, EmailAddress: email, Message: message, EnteredDate: Date.now
    }

    return this.http.post<Contact>(this.accessPointUrl + '/', body, { headers });
  } 
}
