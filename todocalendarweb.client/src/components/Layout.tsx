import { Outlet } from 'react-router-dom';
import { NavMenu } from './NavMenu';

const Layout = () => {
    return (
        <>
            <NavMenu />
            <div className="Container">
                <Outlet/>
            </div>
        </>
    );
}

export { Layout };
