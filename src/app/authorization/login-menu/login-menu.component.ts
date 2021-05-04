import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {

  constructor(private service:SharedService) { }

  ModalTitle:string;
  login:any;
  register:any;
  Authorized:boolean = true;
  ActivateLoginForm:boolean = false;
  ActivateRegisterForm:boolean = false;

  ngOnInit(): void {
  }

  loginClick(){
    this.login={
      Email:"",
      Password:"" 
    }
    this.ModalTitle = "login";
    this.ActivateLoginForm = true;
    this.ActivateRegisterForm = false;
  }
  registerClick(){
    this.register={
      Email:"",     
      UserName:"",
      UserSurname:"",
      UserMiddleName:"",
      PhoneNumber:"",
      Password:"",
      PasswordConfirm:""
    }
    this.ModalTitle = "register";
    this.ActivateRegisterForm = true;
    this.ActivateLoginForm = false;
  }
  closeClick(){
    this.ActivateLoginForm = false;
    this.ActivateRegisterForm = false;
  }
}
