import { Component } from '@angular/core';
import { BasketItemDTO, BasketService, UserBasketDTO } from '../basket.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthorizationService } from '../authorization.service';
import { UserDTO, } from '../authorization/authorization.component';
import { BasketItemInputDTO } from '../basket.service';
import { jwtDecode } from 'jwt-decode';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-basket',
  imports: [CommonModule, FormsModule, RouterLink],
  standalone: true,
  providers: [BasketService, AuthorizationService],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  currentUser: UserDTO | null = null;
  
  basket: UserBasketDTO = {
    basketItems: [],
    totalPrice: 0,
    shippingAddress: '',
    shippingPrice: 5.99
  };
  basketItem: BasketItemInputDTO = {
    albumId: '',
    amount: 0
  }


  constructor(private basketService: BasketService, private authorizationService: AuthorizationService) { }
  ngOnInit() {
    this.getUserBasket();
  }

  getUserBasket(){
    const observer = {
      next: (data: UserBasketDTO) => {
        this.basket = data;
      },
      error: (error: any) => {
        console.error('Error fetching basket:', error);
    }
  }
    this.basketService.getCurrentUserBasket().subscribe(observer);
  }

  updateUserBasket() {
    const observer = {
      next: (data: UserBasketDTO) => {
        this.basket = data ?? { basketItems: [], totalPrice: 0, shippingAddress: '', shippingPrice: 5.99 };
        this.getUserBasket();
      },
      error: (error: any) => {
        console.error('Error updating basket:', error);
    }
  }
    this.basketService.updateUserBasket(this.basket).subscribe(observer);
  }

  removeFromBasket(item: any) {
    const observer = {
      next: (data: UserBasketDTO) => {
        this.basket = data ?? { basketItems: [], totalPrice: 0, shippingAddress: '', shippingPrice: 5.99 };
        this.getUserBasket();
      },
      error: (error: any) => {
        console.error('Error removing item from basket:', error);
      }
    };
    this.basketService.removeFromBasket(item.albumId).subscribe(observer);
  }

  logItem(item: any): void {
    console.log('Item:', item);
  }

}
