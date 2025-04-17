import { Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { SearchComponent } from './search/search.component';
import { AlbumsComponent } from './albums/albums.component';
import { AuthorizationComponent } from './authorization/authorization.component';
import { RegistrationComponent } from './registration/registration.component';
import { AlbumPageComponent } from './album-page/album-page.component';
import { BasketComponent } from './basket/basket.component';

export const routes: Routes = [
    {path: '', component: HomepageComponent},
    {path: 'search', component: SearchComponent},
    {path: 'albums', component: AlbumsComponent},
    {path: 'albums/:id', component: AlbumPageComponent},
    {path: 'login', component: AuthorizationComponent},
    {path: 'register', component: RegistrationComponent},
    {path: 'basket', component: BasketComponent},
    { path: '**', redirectTo: '' }
]