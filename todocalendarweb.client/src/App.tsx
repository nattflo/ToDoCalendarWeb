import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { Home } from './components/Home';


const App = () => {

    return (
        <BrowserRouter>
            <Home/>
        </BrowserRouter>
    );
}

export default App;