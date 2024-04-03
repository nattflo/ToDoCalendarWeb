import { Link } from 'react-router-dom';
import './NavMenu.css';

const NavMenu = () => {

    return (
        <header className='Header'>
            <p className="Logo">FINITE</p>
            <ul className="NavItemsWrapper">
                <li className="NavItem">
                    <Link to='/'>
                        Текущий день
                    </Link>
                </li>
                <li className="NavItem">
                    <Link to='/routines'>
                        Рутины
                    </Link>
                </li>
            </ul>
        </header>
    );
}

export { NavMenu };
