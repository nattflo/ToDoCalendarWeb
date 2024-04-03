import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import './App.css';
import { Home } from './components/Home/Home';
import { RoutineEditor } from './components/RoutineEditor/RoutineEditor';
import { Routines } from './components/Routines/Routines';
import { PeriodsProvider } from './contexts/PeriodContext';
import { RoutinesProvider } from './contexts/RoutineContext';
import { TasksProvider } from './contexts/TasksContext';
import { Layout } from './components/Layout';


const App = () => {

    const router = createBrowserRouter([
        {
            path: '/',
            element: <Layout/>,
            children: [
                {
                    path: "/",
                    element: <Home />
                },
                {
                    path: "/routines/:id",
                    element: <RoutineEditor/>
                },
                {
                    path: '/routines',
                    element: <Routines/>
                }
            ]
        }
    ]);

    return (
        <RoutinesProvider>
            <PeriodsProvider>
                <TasksProvider>
                    <RouterProvider router={router}/>
                </TasksProvider>
            </PeriodsProvider>
        </RoutinesProvider>
    );
}

export default App;