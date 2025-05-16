import { BrowserRouter, Route, Routes } from 'react-router-dom'
import './App.css'
import Login from './pages/Login'
import { AuthProvider } from './contexts/AuthContext'
import { PrivateRoute } from './components/PrivateRoute'
import Layout from './pages/Layout'
import { Products } from './pages/Products'
import { Orders } from './pages/Orders'
import CreateOrder from './pages/CreateOrder'

function App() {

  return (
    <>
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/" element={<Layout />}>
              <Route index element={<PrivateRoute><Products /></PrivateRoute>}></Route>
              <Route path="/products" element={<PrivateRoute><Products /></PrivateRoute>} />
              <Route path="/orders" element={<PrivateRoute><Orders /></PrivateRoute>} />
              <Route path="/orders/create" element={<PrivateRoute><CreateOrder /></PrivateRoute>} />
            </Route>
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </>
  )
}

export default App
