import { useEffect, useState } from "react"
import { listProducts } from "../lib/api";
import type { GetProductListItemDTO } from "../lib/interfaces";
import { Page } from "../components/Page";

export function Products() {

  const [products, setProducts] = useState<GetProductListItemDTO[]>([]);

  useEffect(() => {
    listProducts().then((products) => setProducts(products));
  }, [])

  return (
    <Page
      title="Produse">
      <div className="max-w-2xl">
        <table className="min-w-full divide-y divide-gray-200 text-sm text-gray-700 w-full">
          <thead className="bg-gray-100 text-xs uppercase tracking-wider text-gray-600 text-left">
            <tr>
              <th className="px-6 py-2">ID</th>
              <th className="px-6 py-2">Nume</th>
              <th className="px-6 py-2">Pret</th>
              <th className="px-6 py-2">Stoc</th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-100">
            {products.map(p => (
              <tr key={p.id}>
                <td className="px-6 py-2">{p.id}</td>
                <td className="px-6 py-2">{p.name}</td>
                <td className="px-6 py-2">{p.price}</td>
                <td className="px-6 py-2">{p.stock}</td>
              </tr>
            ))}
          </tbody>
        </table>

      </div>
    </Page>
  )
}