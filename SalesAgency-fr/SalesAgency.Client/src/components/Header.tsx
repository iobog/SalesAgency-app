import { useAuth } from "../contexts/AuthContext";


export default function Header() {
  const { email, logout } = useAuth();

  return (
    <header className="h-14 bg-white px-6 py-2 flex items-center shadow">
      <h1 className="text-xl font-bold flex-1">App</h1>
      <div>
        {email}
        {!email && "Link login"}
        {email && <button className="hover:underline ml-1 cursor-pointer" onClick={logout}>Logout</button>}
      </div>
    </header>
  )
}