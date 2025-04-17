import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  private apiUrl = 'https://localhost:7000/api/album';

  constructor(private http: HttpClient) {}

  getAllAlbums(): Observable<any> {
    return this.http.get(`${this.apiUrl}/all`);
  }
  getRandomAlbums(count: number = 1): Observable<any> {
    return this.http.get(`${this.apiUrl}/random`, { params: { count: count.toString() } });
  }
  searchAlbumByTitleOrArtist(query: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/search?query=${query}`);
  }
  filterAlbumsByGenreOrYear(genre: string[], year: number[]): Observable<any> {
    let params = new HttpParams();
    genre.forEach((g) => {
      params = params.append('genre', g);
    });
    year.forEach((y) => {
      params = params.append('year', y.toString());
    });
    return this.http.get(`${this.apiUrl}/filter`, { params });
  }
  getAlbumById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }
}