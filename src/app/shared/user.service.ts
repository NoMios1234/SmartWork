import { Injectable } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = "https://localhost:44334/api";
  
  registerForm = new FormGroup({
    Email : new FormControl(''),
    Password : new FormControl(''),
  });

  formModel = this.fb.group({
    Email : ['', [Validators.email, Validators.required]],
    UserName : ['', Validators.required],
    UserSurname : [''],
    UserMiddleName : [''],
    PhoneNumber: ['', Validators.pattern("[0-9]{3}[0-9]{3}[0-9]{4}")],
    Passwords : this.fb.group({
      Password : ['', [Validators.required, Validators.minLength(8)]],
      PasswordConfirm : ['', Validators.minLength(8)]
    }, { validator: this.comparePasswords })
    
  });
  
  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('PasswordConfirm');
    if (confirmPswrdCtrl?.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password')?.value != confirmPswrdCtrl?.value)
        confirmPswrdCtrl?.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl?.setErrors(null);
    }
  }

  register() {
    var body = {
      Email: this.formModel.value.Email,
      UserName: this.formModel.value.UserName,
      UserSurname: this.formModel.value.UserSurname,
      UserMiddleName: this.formModel.value.UserMiddleName,
      PhoneNumber: this.formModel.value.PhoneNumber,
      Password: this.formModel.value.Passwords.Password,
      PasswordConfirm: this.formModel.value.Passwords.PasswordConfirm
    };
    return this.http.post(this.BaseURI + '/users/register', body);
  }
}
