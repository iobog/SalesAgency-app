# React + TypeScript + Vite

## 1. Create new react app
```sh
# Create new app
npm create vite@latest my-app -- --template react-ts 

# Navigate to the app directory
cd my-app 
```
See more: https://react.dev/learn/build-a-react-app-from-scratch

## 2. Install packages
```sh
npm install
```

## 3 Install Tailwind CSS
```sh
npm install tailwindcss @tailwindcss/postcss postcss
```
https://tailwindcss.com/docs/installation/framework-guides/nextjs

### 3.2 Create a `postcss.config.mjs` file in the root of your project and add the @tailwindcss/postcss plugin to your PostCSS configuration.
``` js
const config = {
  plugins: {
    "@tailwindcss/postcss": {},
  },
};
export default config;
```

### 3.3 Import Tailwind CSS
Add an `@import` to `./src/app/globals.css` that imports Tailwind CSS.
```css
@import "tailwindcss";

```

## 4. Add Routing 
```sh
npm i -D react-router-dom
```

#### Example:
```tsx
// App.tsx
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

```
Docs: https://www.w3schools.com/react/react_router.asp


## 5. TODO Next
- Validare cantitate produs pe comanda.
- Componenta button.

