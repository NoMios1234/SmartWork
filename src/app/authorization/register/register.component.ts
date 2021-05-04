import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() register:any;
  Id:string;
  Email:string;
  UserName:string;
  UserSurname:string;
  UserMiddleName:string; 
  PhoneNumber:string;
  Password:string;
  PasswordConfirm:string;

  ngOnInit(): void {   
  }

  registerUser(){
    var val = {
      Email:this.Email,
      UserName:this.UserName,               
      UserSurname:this.UserSurname,
      UserMiddleName:this.UserMiddleName,
      PhoneNumber:this.PhoneNumber,           
      Password:this.Password,       
      PasswordConfirm:this.PasswordConfirm
    };
    console.log(val);
    this.service.addUser(val).subscribe(res=>{
      alert('Successfully registered!');
    });
  }

  updateUser(){
    var val = {
      Id:this.Id,
      Email:this.Email,
      UserName:this.UserName,               
      UserSurname:this.UserSurname,
      UserMiddleName:this.UserMiddleName,
      PhoneNumber:this.PhoneNumber,           
      Password:this.Password,       
      PasswordConfirm:this.PasswordConfirm
    };
    this.service.updateUser(val).subscribe(res=>{
      alert('Updated');
    });
  }
}