import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/payments', pathMatch: 'full' },
  { path: 'payments', loadComponent: () => import('./payments/payment-list/payment-list.component').then(m => m.PaymentListComponent) },
];
