import { useNavigate } from 'react-router-dom';
import './style.css'
import { ReactNode } from "react";

interface ModalProps {
    children : ReactNode;
}

export function Modal ({children} : ModalProps) {

    const navigate = useNavigate();
    return (
        <>
            <div 
                className='ModalBackground' 
                onClick={() => {navigate('.')}}
            />
            <div className='Modal'>
                {children}
            </div>
        </>
    )
}