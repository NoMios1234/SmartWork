import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  UserAuth:boolean = false;

  constructor(private router: Router) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') != null)
      this.UserAuth = true;
    if (localStorage.getItem('token') == null)
      this.UserAuth = false;
  }

  onLogout() {
    if(confirm('Are you logout?')){
      this.UserAuth = false;
      localStorage.removeItem('token');
      this.router.navigate(['/user/login']);
    }
  }  
}
