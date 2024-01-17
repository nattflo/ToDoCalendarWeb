import React, { useState } from 'react';
import { Container, Navbar, NavbarBrand, NavbarToggler } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

const NavMenu = () => {
    const [collapsed, setCollapsed] = useState<boolean>(true);
    const toggleNavbar = () => setCollapsed(!collapsed);

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">ToDoCalendar</NavbarBrand>
                    <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                </Container>
            </Navbar>
        </header>
    );
}

export { NavMenu };
