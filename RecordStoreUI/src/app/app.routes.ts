import { Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';

export const routes: Routes = [
    {path: '', component: HomepageComponent},
    { path: '**', redirectTo: '' }, // Redirect unknown routes to the homepage
];
