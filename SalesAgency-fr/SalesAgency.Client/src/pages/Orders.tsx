import { useEffect, useState } from "react";
import type { GetOrderListItemDTO } from "../lib/interfaces";
import { listOrders } from "../lib/api";
import { Link } from "react-router-dom";
import { Page } from "../components/Page";

export function Orders() {
  const [orders, setOrders] = useState<GetOrderListItemDTO[]>([]);

  useEffect(() => {
    listOrders().then((orders) => setOrders(orders));
  }, [])

  const action = (
    <Link to="/orders/create" className="px-4 py-2 text-white bg-green-600 hover:bg-green-700 active:bg-green-800 cursor-pointer">Comanda noua</Link>
  )

  return (
    <Page
      title="Comenzi"
      primaryAction={action}>
      <div className="max-w-2xl">
        <table className="min-w-full divide-y divide-gray-200 text-sm text-gray-700 w-full">
          <thead className="bg-gray-100 text-xs uppercase tracking-wider text-gray-600 text-left">
            <tr>
              <th className="px-6 py-2">ID</th>
              <th className="px-6 py-2">Client</th>
              <th className="px-6 py-2">Nr. Produse</th>
              <th className="px-6 py-2">Total</th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-100">
            {orders.map(o => (
              <tr key={o.id}>
                <td className="px-6 py-2">{o.id}</td>
                <td className="px-6 py-2">{o.client}</td>
                <td className="px-6 py-2">{o.countProducts}</td>
                <td className="px-6 py-2">{o.total} RON</td>
              </tr>
            ))}
          </tbody>
        </table>
        {!orders.length && <div className="px-6 py-2 text-center text-zinc-500">Fara comenzi</div>}
      </div>
    </Page>
  )
}