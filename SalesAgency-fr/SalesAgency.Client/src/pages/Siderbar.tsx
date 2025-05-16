import { Link } from "react-router-dom";

export function Sidebar() {
  return (
    <aside className="w-[300px] px-6 py-3 flex flex-col gap-1">
      <Link to="/products" className="hover:underline">Produse</Link>
      <Link to="/orders" className="hover:underline">Comenzi</Link>
    </aside>
  )
}