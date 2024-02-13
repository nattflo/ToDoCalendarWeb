import { createPortal } from 'react-dom';
import './style.css'
import { CSSProperties, ReactNode } from "react";

interface ModalProps {
    children : ReactNode;
    onClickOutside?: () => void;
    backgroundStyle?: CSSProperties;
    modalStyle?: CSSProperties;
}

export function Modal ({
    children,
    onClickOutside = () => {},
    backgroundStyle,
    modalStyle
} : ModalProps) {

    return (
        createPortal(
            <>
                <div 
                    className={'ModalBackground'}
                    onClick={onClickOutside}
                    style={backgroundStyle}
                />
                <div className='Modal' style={modalStyle}>
                    {children}
                </div>
            </>,
            document.body
        )
    )
}