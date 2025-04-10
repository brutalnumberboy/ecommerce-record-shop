import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { AlbumService } from '../album.service';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  imports: [CommonModule, FormsModule, RouterLink],
  standalone: true,
  providers: [AlbumService],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css'
})
export class SearchComponent implements OnInit{
  searchResults: any[] = [];
  searchQuery: string = '';
  
  constructor(private albumService: AlbumService) {
  }

  ngOnInit(): void {
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
  

  
}