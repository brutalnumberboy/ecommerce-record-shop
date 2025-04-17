import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { AlbumService } from '../album.service';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserDTO } from '../registration/registration.component';
import { AuthorizationService } from '../authorization.service';

@Component({
  selector: 'app-search',
  imports: [CommonModule, FormsModule, RouterLink],
  standalone: true,
  providers: [AlbumService, AuthorizationService],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit{
  searchResults: any[] = [];
  searchQuery: string = '';
  currentUser: UserDTO | null = null;
  
  constructor(private albumService: AlbumService, private authorizationService: AuthorizationService) {
  }

  ngOnInit(): void {
    const observer = {
      next: (data: any) => {
        console.log('Current user:', data);
        this.currentUser = data;
      },
      error: (error: any) => {
        console.error('Error fetching current user:', error);
      }
    }
    this.authorizationService.getCurrentUser().subscribe(observer);
    console.log('Current user:', this.currentUser);
  }
  

  searchAlbums(): void {
    if (!this.searchQuery.trim()) {
        return;
    }
    const observer = {
          next: (data: any) => {
            this.searchResults = data;
          },
          error: (error: any) => {
            console.error('Error fetching search results:', error);
          }
        };
        this.albumService.searchAlbumByTitleOrArtist(this.searchQuery.trim()).subscribe(observer);
    }
  
  logout(): void {
    const observer = {
      next: (data: any) => {
        console.log('Logout successful', data);
        this.currentUser = null;
      },
      error: (error: any) => {
        console.error('Logout error', error);
      }
    }
    this.authorizationService.logout().subscribe(observer);
  }
  
}