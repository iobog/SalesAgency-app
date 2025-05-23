import { CLIENTS_BASE_URL, ORDERS_BASE_URL, PRODUCTS_BASE_URL } from "../utils/const";
import type { CreateOrderDTO, GetClientListItemDTO, GetOrderListItemDTO, GetProductListItemDTO, LoginRequest, LoginResponse } from "./interfaces";

const API_ROOT_URL = "http://localhost:5009/"


export async function login(body: LoginRequest): Promise<LoginResponse> {
  const url = new URL('api/users/login', API_ROOT_URL);
  const response = await fetch(url.toString(), {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || 'Login failed');
  }

  return await response.json();
}



export async function listClients(): Promise<GetClientListItemDTO[]> {
  const url =  CLIENTS_BASE_URL;
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
  const url = ORDERS_BASE_URL;
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
  const url = PRODUCTS_BASE_URL;
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