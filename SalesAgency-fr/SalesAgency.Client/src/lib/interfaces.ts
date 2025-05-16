
// Account
export interface LoginRequest {
  user: string;
  pass: string;
}

export interface LoginResponse {
  email: string;
  token: string;
}

// Client
export type GetClientListItemDTO = {
  id: number;
  name: string;
}

// Product
export interface GetProductListItemDTO {
  id: number;
  name: string;
  description?: string;
  price: number;
  stock: number;
}

// Order 
export interface GetOrderListItemDTO {
  id: number;
  client: string;
  createdAt: Date;
  total: number;
  countProducts: number;
  user: string;
}

export interface CreateOrderProductDTO {
  productId: number;
  name: string;
  price: number;
  quantity: number;
}

export interface CreateOrderDTO {
  clientId: number;
  address: string;
  products: CreateOrderProductDTO[];
}
