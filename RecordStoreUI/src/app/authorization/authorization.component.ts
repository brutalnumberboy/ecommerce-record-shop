import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthorizationService } from '../authorization.service';

export interface UserDTO {
    userName: string;
    email: string;
    token: string;
}

@Component({
  selector: 'app-authorization',
  imports: [CommonModule, FormsModule, RouterModule],
  standalone: true,
  providers: [AuthorizationService],
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.css']
})
export class AuthorizationComponent implements OnInit{
  errorMessage: string | null = null;
  loginData = {
    email: '',
    password: ''
  };

  constructor(private authorizationService: AuthorizationService, private router: Router) {
  }

  ngOnInit(): void {
  }

  login() {
    const observer = {
      next: (data: any) => {
        this.errorMessage = null;
        console.log('Login successful', data);
        const token = data.token;
        localStorage.setItem('jwt', token); 
        this.router.navigate(['/']);
      },
      error: (err: any) => {
        if (err.error && err.error.errors) {
          this.errorMessage = Object.values(err.error.errors).join(' ');
        } else if (err.error && err.error.message) {
          this.errorMessage = err.error.message;
        } else {
          this.errorMessage = 'Login failed. Please try again.';
      }
    }
  }
  this.authorizationService.login(this.loginData.email, this.loginData.password).subscribe(observer);
}
}