import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { AlbumService } from '../album.service';

@Component({
  selector: 'app-homepage',
  imports: [CommonModule],
  standalone: true,
  providers: [AlbumService],
  // Note: 'providers' is used here to provide the AlbumService for this component only.
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent implements OnInit {
  randomAlbums: any[] = [];

  constructor(private albumService: AlbumService) {
    console.log('HomepageComponent constructor called'); // Debugging
  }

  ngOnInit(): void {
    this.fetchRandomAlbums();
    console.log('HomepageComponent initialized'); // Debugging
  }

  fetchRandomAlbums(): void {
    const observer = {
      next: (data: any) => {
        console.log('Random albums fetched:', data); // Debugging
        this.randomAlbums = data;
      },
      error: (error: any) => {
        console.error('Error fetching random albums:', error);
      }
    }
    this.albumService.getRandomAlbums(4).subscribe(observer);
  }

}
