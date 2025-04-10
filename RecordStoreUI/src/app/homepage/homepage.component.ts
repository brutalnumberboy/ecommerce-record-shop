import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { AlbumService } from '../album.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-homepage',
  imports: [CommonModule, RouterLink],
  standalone: true,
  providers: [AlbumService],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent implements OnInit {
  randomAlbums: any[] = [];

  constructor(private albumService: AlbumService) {
  }

  ngOnInit(): void {
    this.getRandomAlbums();
  }

  getRandomAlbums(): void {
    const observer = {
      next: (data: any) => {
        this.randomAlbums = data;
      },
      error: (error: any) => {
        console.error('Error fetching random albums:', error);
      }
    }
    this.albumService.getRandomAlbums(4).subscribe(observer);
  }

}
