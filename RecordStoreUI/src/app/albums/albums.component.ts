import { Component } from '@angular/core';
import { AlbumService } from '../album.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-albums',
  imports: [CommonModule, RouterLink],
  standalone: true,
  providers: [AlbumService],
  templateUrl: './albums.component.html',
  styleUrl: './albums.component.css'
})
export class AlbumsComponent {
  genres: string[] = [];
  selectedGenres: string[] = [];
  selectedYears: number[] = [];
  years: number[] = [];
  albums: any[] = [];
  filteredAlbums: any[] = [];

  constructor (private albumService: AlbumService) {
  }
  ngOnInit(): void {
    this.getGenres();
    this.getYears();
    this.getAlbums();
  }

  getAlbums(): void {
    const observer = {
      next: (data: any[]) => {
        this.albums = data;
        this.filteredAlbums = [...this.albums];

      },
      error: (error: any) => {
        console.error('Error fetching albums:', error);
      }
    };
    this.albumService.getAllAlbums().subscribe(observer);
  }

  getGenres(): void {
    const observer = {
      next: (data: any[]) => {
        const genres = new Set<string>();
        data.forEach(album => {
          if (album.genre) {
            genres.add(album.genre);
          }
        }
        );
        this.genres = Array.from(genres);
      },
      error: (error: any) => {
        console.error('Error fetching genres:', error);
      }
    };
    this.albumService.getAllAlbums().subscribe(observer);
  }

  getYears(): void {
    const observer = {
      next: (data: any[]) => {
        const years = new Set<number>();
        data.forEach(album => {
          if (album.yearReleased) {
            years.add(album.yearReleased);
          }
        });
        this.years = Array.from(years);
        console.log(this.years);
      },
      error: (error: any) => {
        console.error('Error fetching years:', error);
      }
    };
    this.albumService.getAllAlbums().subscribe(observer);
  }

  onGenreChange(event: any): void {
    const genre = event.target.value;
    console.log('onGenreChange called, genre:', genre, 'checked:', event.target.checked);
    if (event.target.checked){
      this.selectedGenres.push(genre);
    } else {
      this.selectedGenres = this.selectedGenres.filter(g => g !== genre);
    }
    this.filterAlbumsByYearAndGenre()
  }
  onYearChange(event: any): void {
    const year = event.target.value;
    if (event.target.checked){
      this.selectedYears.push(year);
    } else {
      this.selectedYears = this.selectedYears.filter(y => y !== year);
    }
    this.filterAlbumsByYearAndGenre();
  }

  filterAlbumsByYearAndGenre() {
    if (this.selectedGenres.length > 0 || this.selectedYears.length > 0) {
      const observer = {
        next: (data: any) => {
          this.filteredAlbums = data;
        },
        error: (error: any) => {
          console.error('Error fetching filtered albums:', error);
        }
      };
      this.albumService.filterAlbumsByGenreOrYear(this.selectedGenres, this.selectedYears).subscribe(observer);
    } else {
      this.filteredAlbums = [...this.albums];
    }      
  }
 
}
