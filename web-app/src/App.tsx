import React from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { Chat, SignIn, SignUp } from './pages';

const router = createBrowserRouter([
  {
    path: "/",
    element: <SignIn />,
  },
  {
    path: "/signup",
    element: <SignUp />,
  },
  {
    path: "/chat",
    element: <Chat />,
  }
]);

function App() {
  return (
    <RouterProvider router={router} />
  );
}

export default App;
