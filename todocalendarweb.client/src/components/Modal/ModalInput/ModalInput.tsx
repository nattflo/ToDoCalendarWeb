import { CSSProperties } from 'react'
import { EditableText } from '../../EditableText/EditableText'
import { Modal } from '../modal'
import './style.css'

interface ModalInputProps{
    placeholder: string,
    onChange?: (output: string) => void
    onClose?: () => void
}

export const ModalInput = ({
    placeholder,
    onChange = () => {},
    onClose = () => {}
}: ModalInputProps) => {

    const backgroundStyle: CSSProperties = {
        backgroundColor: 'unset',
    }

    const modalStyle: CSSProperties = {
        height: 'fit-content',
        width: 'unset',
        right: '5px',
        left: '5px',
        bottom: '5px',
        borderRadius: '10px',
        padding: '5px 10px 5px 10px'

    }

    return (
        <Modal
            backgroundStyle={backgroundStyle}
            modalStyle={modalStyle}
            onClickOutside={onClose}
            >
            <EditableText
                tag='p'
                placeholder={placeholder}
                onChange={onChange}
            />
        </Modal>
    )
}