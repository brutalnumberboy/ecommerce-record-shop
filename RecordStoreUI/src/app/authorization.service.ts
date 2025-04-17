import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserDTO } from './authorization/authorization.component';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private apiUrl = 'https://localhost:7000/api/Authorization';

  constructor(private http: HttpClient) { }

  login(email: string, password: string) {
    const url = `${this.apiUrl}/login`;
    return this.http.post(url, { email, password }, {withCredentials: true});
  }
  
  register(userName: string, email: string, password: string) {
    const url = `${this.apiUrl}/register`;
    return this.http.post(url, { userName, email, password }, {withCredentials: true});
  }
  
  getCurrentUser(): Observable<UserDTO>{
    const url = `${this.apiUrl}/current`;
    return this.http.get<UserDTO>(url, {withCredentials: true});
  }
  logout(): Observable<any> {
    const url = `${this.apiUrl}/logout`;
    return this.http.post(url, {}, { withCredentials: true });
  }
}
