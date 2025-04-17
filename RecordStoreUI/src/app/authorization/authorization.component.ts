import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthorizationService } from '../authorization.service';

export interface UserDTO {
    userName: string;
    email: string;
    token: string;
}

@Component({
  selector: 'app-authorization',
  imports: [CommonModule, FormsModule, RouterLink, RouterModule],
  standalone: true,
  providers: [AuthorizationService],
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.css']
})
export class AuthorizationComponent implements OnInit{
  loginData = {
    email: '',
    password: ''
  };

  constructor(private authorizationService: AuthorizationService) {
  }

  ngOnInit(): void {
  }

  login() {
    const observer = {
      next: (data: any) => {
        console.log('Login successful', data);
        const token = data.token;
        localStorage.setItem('jwt', token); // Store the JWT in local storage
      },
      error: (err: any) => {
        console.error('Login error', err);
      }
    }
    this.authorizationService.login(this.loginData.email, this.loginData.password).subscribe(observer);
  }

  logout(){
    const observer = {
      next: (data: any) => {
        console.log('Logout successful', data);
      },
      error: (err: any) => {
        console.error('Logout error', err);
      }
    }
    this.authorizationService.logout().subscribe(observer);
  }
}