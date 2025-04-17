import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthorizationService } from '../authorization.service';

export interface UserDTO {
    userName: string;
    email: string;
    token: string;
}

@Component({
  selector: 'app-registration',
  imports: [CommonModule, FormsModule, RouterModule],
  standalone: true,
  providers: [AuthorizationService],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit{

  registerData = {
    userName: '',
    email: '',
    password: ''
  };

  constructor(private authorizationService: AuthorizationService) {
  }

  ngOnInit(): void {
  }

  register() {
    const observer = {
      next: (data: any) => {
        console.log('Registration successful', data);
        const token = data.token;
        localStorage.setItem('jwt', token); // Store the JWT in local storage

      },
      error: (err: any) => {
        console.error('Registration error', err);
      }
    }
    this.authorizationService.register(this.registerData.userName, this.registerData.email, this.registerData.password).subscribe(observer);
  }

}