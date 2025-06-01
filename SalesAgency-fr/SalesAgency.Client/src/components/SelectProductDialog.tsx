import { useEffect, useState } from "react";
import Dialog from "./Dialog";
import type { GetProductListItemDTO } from "../lib/interfaces";
import { listProducts } from "../lib/api";

export default function SelectProductDialog({ isOpen, onClose, onSelect }: any) {

  const [products, setProducts] = useState<GetProductListItemDTO[]>([])
  const [selectedProduct, setSelectedProduct] = useState<GetProductListItemDTO | null>(null);

  useEffect(() => {
    listProducts().then((products) => {
      setProducts(products)
    })
  }, [isOpen == true])

  const onCancel = () => {
    setSelectedProduct(null);
    onClose();
  }

  const onOK = () => {
    onSelect(selectedProduct);
    setSelectedProduct(null);
  }

  return (
    <Dialog
      isOpen={isOpen}
      onClose={onCancel}>
      <h2 className="text-lg px-6 py-4">Alege produs</h2>
      <div className="">
        <div className="divide-y divide-zinc-100">
          {products.map(p =>
            <label className="text-medium py-2 flex hover:bg-zinc-100 px-6 cursor-pointer" key={p.id}>
              <input
                className=""
                type="radio"
                name="product"
                value={p.id}
                checked={selectedProduct?.id === p.id}
                onChange={() => setSelectedProduct(p)}
              />
              <div className="pl-2">
                <div>{p.name}</div>
                <div className="text-xs text-zinc-500">
                  {p.price} RON • {p.stock} bucati
                </div>
              </div>
            </label>
          )}
        </div>
      </div>
      <div className="px-6 py-4 flex gap-1 justify-end">
        <button className="bg-zinc-200 hover:bg-zinc-300 px-4 py-2 cursor-pointer" onClick={onCancel}>Renunta</button>
        {selectedProduct && <button className="bg-green-600 hover:bg-green-700 px-4 py-2 cursor-pointer text-white" onClick={onOK}>OK</button>}
      </div>
    </Dialog>
  )
}