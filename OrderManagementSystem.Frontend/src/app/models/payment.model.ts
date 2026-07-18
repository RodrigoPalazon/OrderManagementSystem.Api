export interface Payment {
  id: number;
  orderId: number;
  amount: number;
  paymentDate: string;
  paymentMethod: string;
  status: string;
}
