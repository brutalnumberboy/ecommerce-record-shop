import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthorizationService } from '../authorization.service';

export interface UserDTO {
    userName: string;
    email: string;
    token: string;
}

@Component({
  selector: 'app-registration',
  imports: [CommonModule, FormsModule],
  standalone: true,
  providers: [AuthorizationService],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit{
  successMessage: string | null = null;
  errorMessage: string | null = null;
  registerData = {
    userName: '',
    email: '',
    password: ''
  };

  constructor(private authorizationService: AuthorizationService, private router: Router) {
  }

  ngOnInit(): void {
  }

  register() {
    const observer = {
      next: (data: any) => {
        this.errorMessage = null;
        this.successMessage = 'Registration is successful';
        const token = data.token;
        localStorage.setItem('jwt', token); 
      },
      error: (err: any) => {
        this.successMessage = null;
        if (err.error && err.error.errors) {
          this.errorMessage = Object.values(err.error.errors).join(' ');
        } else if (err.error && err.error.message) {
          this.errorMessage = err.error.message;
        } else {
          this.errorMessage = 'Registration failed. Please try again.';
        }
      }
    }
    this.authorizationService.register(this.registerData.userName, this.registerData.email, this.registerData.password).subscribe(observer);
  }

}