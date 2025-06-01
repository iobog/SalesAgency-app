// import { useEffect, useState } from "react";
// import { Page } from "../components/Page";
// import { type GetProductListItemDTO, type CreateOrderProductDTO, type GetClientListItemDTO, type CreateOrderDTO } from "../lib/interfaces";
// import SelectProductDialog from "../components/SelectProductDialog";
// import { listClients } from "../lib/api";
// import Card from "../components/Card";

// export default function CreateOrder() {

//   const [clients, setClients] = useState<GetClientListItemDTO[]>([]);
  
//   const [orderProducts, setOrderProducts] = useState<CreateOrderProductDTO[]>([]);
//   const [clientId, setClientId] = useState<number | null>(null);
//   const [address, setAddress] = useState<string | null>(null);

//   const [openDialog, setOpenDialog] = useState(false);


//   const addProduct = (product: GetProductListItemDTO) => {
//     var index = orderProducts.findIndex(op => op.productId == product.id)
//     if (index != -1) {
//       orderProducts[index].quantity++;
//     }
//     else {
//       setOrderProducts([
//         ...orderProducts,
//         {
//           productId: product.id,
//           name: product.name,
//           price: product.price,
//           quantity: 1
//         }
//       ])
//     }
//   }

//   useEffect(() => {
//     listClients().then((clients) => {
//       setClients(clients)
//     })
//   }, [])

//   const onSave = () => {

//     const body: CreateOrderDTO = {
//       clientId: clientId!,
//       address: address!,
//       products: orderProducts
//     }

//     console.log("Saving the order...", body);
//   }


//   return (
//     <Page
//       title="Comanda noua">
//       <div className="max-w-2xl">

//         <Card
//           title="Client">
//           <div className="px-6">
//             <select
//               onChange={(e) => setClientId(parseInt(e.target.value))}>
//               {clients.map(c =>
//                 <option key={c.id} value={c.id}>{c.name}</option>
//               )}
//             </select>
//           </div>
//         </Card>
//         <br />

//         <Card
//           title="Adresa">
//           <div className="px-6">
//             <textarea className="w-full" onChange={(e) => setAddress(e.target.value)}></textarea>
//           </div>
//         </Card>
//         <br />

//         <Card
//           title="Produse"
//           action={
//             <button
//               className="text-green-600 bg-green-600/10 hover:bg-green-600/20 active:bg-green-600/25 px-3 py-1 cursor-pointer"
//               onClick={() => setOpenDialog(true)}>Produs nou</button>
//           }>
//           <table className="min-w-full divide-y divide-gray-200 text-sm text-gray-700 w-full">
//             <thead className="text-xs uppercase tracking-wider text-gray-600 text-left">
//               <tr>
//                 <th className="px-6 py-2">ID</th>
//                 <th className="px-6 py-2">Nume</th>
//                 <th className="px-6 py-2">Pret</th>
//                 <th className="px-6 py-2">Cantitate</th>
//                 <th className="px-6 py-2">Total</th>
//               </tr>
//             </thead>
//             <tbody className="bg-white divide-y divide-gray-100">
//               {orderProducts.map(op => (
//                 <tr key={op.productId}>
//                   <td className="px-6 py-2">{op.productId}</td>
//                   <td className="px-6 py-2">{op.name}</td>
//                   <td className="px-6 py-2">{op.price}</td>
//                   <td className="px-6 py-2">{op.quantity}</td>
//                   <td className="px-6 py-2">{op.quantity * op.price}</td>
//                 </tr>
//               ))}
//             </tbody>
//           </table>
//           {!orderProducts.length && <div className="px-6 py-2 text-center text-zinc-500">Fara produse</div>}
//         </Card>

//         <br />
//         <br />

//         <button className="bg-green-600 text-white px-4 py-2 cursor-pointer w-full" onClick={onSave}>Salveaza</button>



//         <SelectProductDialog
//           isOpen={openDialog}
//           onClose={() => {
//             setOpenDialog(false);
//           }}
//           onSelect={(p: GetProductListItemDTO) => {
//             setOpenDialog(false);
//             addProduct(p);
//           }} />



//       </div>
//     </Page>
//   )
// }