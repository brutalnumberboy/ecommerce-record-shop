import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { AlbumService } from '../album.service';
import { AuthorizationService } from '../authorization.service';
import { RouterLink } from '@angular/router';
import { UserDTO } from '../authorization/authorization.component';


@Component({
  selector: 'app-homepage',
  imports: [CommonModule, RouterLink],
  standalone: true,
  providers: [AlbumService, AuthorizationService],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent implements OnInit {
  randomAlbums: any[] = [];
  currentUser: UserDTO | null = null;

  constructor(private albumService: AlbumService, private authorizationService: AuthorizationService) { }

  ngOnInit(): void {
    this.getRandomAlbums();
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
