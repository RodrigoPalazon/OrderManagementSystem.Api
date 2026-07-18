import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../models/payment.model';

@Component({
  selector: 'app-payment-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './payment-list.component.html',
  styleUrl: './payment-list.component.css'
})
export class PaymentListComponent implements OnInit {
  private paymentService = inject(PaymentService);
  payments: Payment[] = [];

  ngOnInit(): void {
    this.loadPayments();
  }

  loadPayments(): void {
    this.paymentService.getPayments().subscribe({
      next: (data) => this.payments = data,
      error: (err) => console.error('Error loading payments', err)
    });
  }

  deletePayment(id: number): void {
    if (confirm('Are you sure you want to delete this payment?')) {
      this.paymentService.deletePayment(id).subscribe({
        next: () => this.loadPayments(),
        error: (err) => console.error('Error deleting payment', err)
      });
    }
  }
}
