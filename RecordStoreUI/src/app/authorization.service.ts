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
    return this.http.post(`${this.apiUrl}/login`, { email, password }, {withCredentials: true});
  }
  
  register(userName: string, email: string, password: string) {
    return this.http.post(`${this.apiUrl}/register`, { userName, email, password }, {withCredentials: true});
  }
  
  getCurrentUser(): Observable<UserDTO>{
    return this.http.get<UserDTO>(`${this.apiUrl}/current`, {withCredentials: true});
  }
  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/logout`, {}, { withCredentials: true });
  }
}
