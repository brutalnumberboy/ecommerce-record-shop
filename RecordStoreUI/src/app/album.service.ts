import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  private apiUrl = 'http://localhost:5230/api/album';

  constructor(private http: HttpClient) {}

  getRandomAlbums(count: number = 2): Observable<any> {
    return this.http.get(`${this.apiUrl}/random`, { params: { count: count.toString() } });
  }
}