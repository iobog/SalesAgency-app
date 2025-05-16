import { Outlet } from "react-router-dom";
import Header from "../components/Header";
import { Sidebar } from "./Siderbar";

export default function Layout() {
  return (
    <div className="flex flex-col h-full bg-zinc-100">
      <Header />
      <div className="flex-1 flex flex-row">
        <Sidebar />
        <main className="px-6 py-3 flex-1">
          <Outlet />
        </main>
      </div>
    </div>
  )
}