import type { CreateOrderDTO, GetClientListItemDTO, GetOrderListItemDTO, GetProductListItemDTO, LoginRequest, LoginResponse } from "./interfaces";

const API_ROOT_URL = ""

export async function login(body: LoginRequest): Promise<LoginResponse> {
  const url = new URL(`api/account/login`, API_ROOT_URL);
  const request = await fetch(url.toString(), {
    method: 'POST',
    body: JSON.stringify(body)
  })
  const response: LoginResponse = await request.json();
  return response;
}

export async function listClients(): Promise<GetClientListItemDTO[]> {
  const url = '/clients.json';
  const token = localStorage.getItem('token');
  const request = await fetch(url.toString(), {
    method: 'GET',
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  })
  const response: GetClientListItemDTO[] = await request.json();
  return response;
}

export async function listOrders(): Promise<GetOrderListItemDTO[]> {
  const url = '/orders.json';
  const token = localStorage.getItem('token');
  const request = await fetch(url.toString(), {
    method: 'GET',
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  })
  const response: GetOrderListItemDTO[] = await request.json();
  return response;
}

export async function listProducts(): Promise<GetProductListItemDTO[]> {
  const url = '/products.json';
  const token = localStorage.getItem('token');
  const request = await fetch(url.toString(), {
    method: 'GET',
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  })
  const response: GetProductListItemDTO[] = await request.json();
  return response;
}

export async function createOrder(body: CreateOrderDTO): Promise<void> {
  const url = new URL(`api/orders`, API_ROOT_URL);
  const token = localStorage.getItem('token');
  const request = await fetch(url.toString(), {
    method: 'POST',
    body: JSON.stringify(body),
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  })
  const response = await request.json();
  return response;
}