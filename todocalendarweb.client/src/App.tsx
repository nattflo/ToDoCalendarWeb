import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import './App.css';
import { Home } from './components/Home';
import { RoutineEditor } from './components/RoutineEditor/RoutineEditor';


const App = () => {

    const router = createBrowserRouter([
        {
          path: "/",
          element: <Home />
        },
        {
            path: "/routines/:id",
            element: <RoutineEditor/>
        }
      ]);

    return (
        <RouterProvider router={router}/>
    );
}

export default App;