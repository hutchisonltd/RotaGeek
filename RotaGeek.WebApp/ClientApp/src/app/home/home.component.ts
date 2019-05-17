import { Component, OnInit } from '@angular/core';
import { ContactService, Contact } from '../contact.service';
import { ProjectionState } from '@angular/core/src/render3/interfaces';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Meta, Title } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  Contacts: any = [];
  name: string;
  email: string;
  message: string;

  constructor(private contactservice: ContactService, private httpClient: HttpClient, private titleService: Title, private meta: Meta) {
    
  }

  ngOnInit() {
    this.getContacts();
  }

  getContacts() {
    this.Contacts = [];

    this.contactservice.get().subscribe((data: {}) => {
      console.log(data);
      this.Contacts = data;
    });

  }

  processForm() {

    this.contactservice.createContact(this.name, this.email, this.message).subscribe(() => {

      alert('Success');
    });
  }
}
