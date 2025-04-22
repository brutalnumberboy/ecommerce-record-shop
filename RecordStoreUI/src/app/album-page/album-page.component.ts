import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AlbumService } from '../album.service';
import { BasketService } from '../basket.service';
import { UserDTO } from '../registration/registration.component';
import { AuthorizationService } from '../authorization.service';
import { jwtDecode } from 'jwt-decode';


@Component({
  selector: 'app-album-page',
  imports: [CommonModule],
  standalone: true,
  providers: [AlbumService, BasketService, AuthorizationService],
  templateUrl: './album-page.component.html',
  styleUrl: './album-page.component.css'
})
export class AlbumPageComponent {
  album: any;
  currentUser: UserDTO | null = null;

  constructor(private route: ActivatedRoute, private http: HttpClient, private albumService: AlbumService, private basketService: BasketService, private authorizationService: AuthorizationService) {}

  ngOnInit(): void {
    const albumId = this.route.snapshot.paramMap.get('id');
    console.log('Album ID:', albumId);
    if (albumId) {
      this.fetchAlbumDetails(albumId);
    }
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
  }

  addToBasket(): void {
    if (!this.album) {
      console.error('No album loaded to add to basket.');
      return;
    }
    const token = localStorage.getItem('jwt');
    if (!token) {
        console.error('JWT token not found in cookies. User might not be logged in.');
        return;
    }
    let userId: string;
    try {
      const decodedToken: any = jwtDecode(token);
      userId = decodedToken.nameid;
    } catch (error) {
      console.error('Error decoding JWT token:', error);
      return;
    }

    const basketItem = {
      albumId: this.album.id.toString(),
      title: this.album.title,
      artist: this.album.artist,
      price: this.album.price,
      userId: userId,
      amount: 1 
    };
    const observer = {
      next: (data: any) => {
        console.log('Item added to basket:', data);
      },
      error: (error: any) => {
        console.error('Error adding item to basket:', error);
      }
    }
    this.basketService.addToBasket(basketItem).subscribe(observer);
  }

  fetchAlbumDetails(id: string): void {
    const observer = {
      next: (data: any) => {
        this.album = data;
        console.log('Album details:', this.album);
      },
      error: (error: any) => {
        console.error('Error fetching album details:', error);
      }
    }
    this.albumService.getAlbumById(id).subscribe(observer);
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
