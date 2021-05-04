import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.css']
})
export class AuthorizationComponent implements OnInit {

  constructor(private service:SharedService) { }

  isAuthorized:boolean;

  ngOnInit(): void {
    
    this.service.getAuthorized().subscribe(res =>{

    });
  }
}
