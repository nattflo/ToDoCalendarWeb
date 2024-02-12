import React from 'react';
import { NavMenu } from './NavMenu';

interface Props {
  children?: React.ReactNode;
}

const Layout = ({ children }: Props) => {
    return (
        <div>
            <NavMenu />
            {children}
        </div>
    );
}

export { Layout };
