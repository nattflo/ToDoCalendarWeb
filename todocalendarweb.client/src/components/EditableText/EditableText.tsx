import { ElementType, KeyboardEvent, useEffect, useRef, useState } from "react"
import './style.css'
interface EditableTextProps {
    tag: ElementType
    text?: string
    placeholder?: string
    onChange?: (text: string) => void
    isFocus?: boolean
    isClear?: boolean
}

export const EditableText = ({tag: Wrapper, text='', placeholder='Input', onChange = () => {}, isFocus= false, isClear= false}: EditableTextProps) => {

    const [currentText, setCurrentText] = useState(text)

    const ref = useRef<HTMLElement| null>(null)

    useEffect(() => {
        isFocus &&
        ref.current?.focus()
    }, [ref, isFocus])

    const handleKeyDown = (event: KeyboardEvent) => {
        const element = (event.currentTarget as HTMLElement);
        if (event.key === 'Enter'){
            
            element.blur();
            applyText(element);
            event.preventDefault();
        }
    }
    const applyText = (element: HTMLElement) => {
        const input = element.innerText;

        if(input.length > 0 && input != currentText){

            setCurrentText(input)
            onChange(input)
        }else element.innerText = currentText

        isFocus &&
        ref.current?.focus()

        isClear &&
        setCurrentText('')
        element.innerText = ''
    }

    return (
        <div>
            <Wrapper
                ref={ref}
                className='Editable'
                suppressContentEditableWarning={true}
                contentEditable
                onKeyDown={handleKeyDown}
                data-placeholder={placeholder}
            >
                {currentText}
            </Wrapper>
        </div>
    )
}