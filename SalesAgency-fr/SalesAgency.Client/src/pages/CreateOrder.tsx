import { useEffect, useState } from "react";
import { Page } from "../components/Page";
import {
  type GetProductListItemDTO,
  type CreateOrderProductDTO,
  type GetClientListItemDTO,
  type CreateOrderDTO,
} from "../lib/interfaces";
import SelectProductDialog from "../components/SelectProductDialog";
import { listClients, createOrder } from "../lib/api";
import Card from "../components/Card";

export default function CreateOrder() {
  const [clients, setClients] = useState<GetClientListItemDTO[]>([]);
  const [orderProducts, setOrderProducts] = useState<CreateOrderProductDTO[]>([]);
  const [clientId, setClientId] = useState<number | null>(null);
  const [address, setAddress] = useState<string>("");
  const [openDialog, setOpenDialog] = useState(false);

  useEffect(() => {
    listClients().then((clients) => {
      setClients(clients);
    });
  }, []);

  const addProduct = (product: GetProductListItemDTO) => {
    setOrderProducts((prev) => {
      const index = prev.findIndex((op) => op.productId === product.id);
      if (index !== -1) {
        const updated = [...prev];
        updated[index] = {
          ...updated[index],
          quantity: updated[index].quantity + 1,
        };
        return updated;
      }
      return [
        ...prev,
        {
          productId: product.id,
          name: product.name,
          price: product.price,
          quantity: 1,
        },
      ];
    });
  };

  const onDelete = (productId: number) => {
    setOrderProducts((prev) => prev.filter((op) => op.productId!= productId));
  };

  const onSave = async () => {
    if (!clientId || !address.trim() || orderProducts.length === 0) {
      alert("Completati toate campurile si adaugati cel putin un produs.");
      return;
    }

    const body: CreateOrderDTO = {
      clientId,
      address,
      products: orderProducts,
    };

    try {
      await createOrder(body);
      alert("Comanda a fost salvata cu succes!");
      setClientId(null);
      setAddress("");
      setOrderProducts([]);
    } catch (error: any) {
      console.log(error);
      
      alert("Eroare la salvarea comenzii.");
    }
  };

  return (
    <Page title="Comanda noua">
      <div className="max-w-2xl">
        <Card title="Client">
          <div className="px-6">
            <select
              className="w-full"
              value={clientId ?? ""}
              onChange={(e) => setClientId(parseInt(e.target.value))}
            >
              <option value="">Selecteaza un client</option>
              {clients.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.name}
                </option>
              ))}
            </select>
          </div>
        </Card>

        <br />

        <Card title="Adresa">
          <div className="px-6">
            <textarea
              className="w-full"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
            ></textarea>
          </div>
        </Card>

        <br />

        <Card
          title="Produse"
          action={
            <button
              className="text-green-600 bg-green-600/10 hover:bg-green-600/20 active:bg-green-600/25 px-3 py-1 cursor-pointer"
              onClick={() => setOpenDialog(true)}
            >
              Produs nou
            </button>
          }
        >
          <table className="min-w-full divide-y divide-gray-200 text-sm text-gray-700 w-full">
            <thead className="text-xs uppercase tracking-wider text-gray-600 text-left">
              <tr>
                <th className="px-6 py-2">ID</th>
                <th className="px-6 py-2">Nume</th>
                <th className="px-6 py-2">Pret</th>
                <th className="px-6 py-2">Cantitate</th>
                <th className="px-6 py-2">Total</th>
                <th className="px-6 py-2">Sterge</th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-100">
              {orderProducts.map((op) => (
                <tr key={op.productId}>
                  <td className="px-6 py-2">{op.productId}</td>
                  <td className="px-6 py-2">{op.name}</td>
                  <td className="px-6 py-2">{op.price}</td>
                  <td className="px-6 py-2">{op.quantity}</td>
                  <td className="px-6 py-2">{op.quantity * op.price}</td>
                  <td className="px-6 py-2">
                    <button type="button" 
                      className="text-white bg-red-700 hover:bg-red-800 focus:outline-none focus:ring-4 focus:ring-red-300 font-medium rounded-full text-sm px-5 py-2.5 text-center me-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900"
                      onClick={() => onDelete(op.productId)}>

                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          {!orderProducts.length && (
            <div className="px-6 py-2 text-center text-zinc-500">Fara produse</div>
          )}
        </Card>

        <br />
        <br />

        <button
          className="bg-green-600 text-white px-4 py-2 cursor-pointer w-full"
          onClick={onSave}
        >
          Salveaza
        </button>

        <SelectProductDialog
          isOpen={openDialog}
          onClose={() => {
            setOpenDialog(false);
          }}
          onSelect={(p: GetProductListItemDTO) => {
            setOpenDialog(false);
            addProduct(p);
          }}
        />
      </div>
    </Page>
  );
}
