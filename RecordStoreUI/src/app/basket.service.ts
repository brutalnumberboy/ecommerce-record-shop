import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


export interface BasketItemDTO {
  albumId: string;
  title: string;
  artist: string;
  price: number;
  amount: number;
}

export interface UserBasketDTO {
  basketItems: BasketItemDTO[];
  totalPrice: number;
  shippingAddress: string;
  shippingPrice: number;
}

export interface BasketItemInputDTO {
  albumId: string;
  amount: number;
}

export interface UserBasketInputDTO {
  basketItems: BasketItemDTO[];
  shippingAddress: string;
  shippingPrice: number;
}

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private api = 'https://localhost:7000/api/basket';

  constructor(private http: HttpClient) { }

  updateUserBasket(basket: UserBasketDTO): Observable<UserBasketDTO> {
    return this.http.put<UserBasketDTO>(`${this.api}/current`, basket, {withCredentials: true});
  }
  getCurrentUserBasket(): Observable<UserBasketDTO> {
    return this.http.get<UserBasketDTO>(`${this.api}/current`, {withCredentials: true});
  }
  addToBasket(basketItem: BasketItemDTO): Observable<UserBasketDTO> {
    return this.http.post<UserBasketDTO>(`${this.api}/add`, basketItem, {withCredentials: true});
  }
  removeFromBasket(albumId: string): Observable<UserBasketDTO> {
    return this.http.post<UserBasketDTO>(`${this.api}/remove?albumId=${albumId}`, {}, {withCredentials: true});
  }
}
