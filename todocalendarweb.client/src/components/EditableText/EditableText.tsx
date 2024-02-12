import { KeyboardEvent, useState } from "react";
import './style.css';

interface EditableTextProps {
    tag: keyof JSX.IntrinsicElements;
    text?: string;
    placeholder?: string;
    onChange?: (text: string) => void;
}

export const EditableText = ({tag : Wrapper, text='', placeholder='Input', onChange = () => {}}: EditableTextProps) => {

    const [currentText, setCurrentText] = useState(text);

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

            setCurrentText(input);
            onChange(input);
        }else element.innerText = currentText;
    }

    return (
        <div>
            <Wrapper
                className='Editable'
                suppressContentEditableWarning={true}
                contentEditable
                onKeyDown={handleKeyDown}
                onChange={() => console.log('change')}
                data-placeholder={placeholder}
            >
                {currentText}
            </Wrapper>
        </div>
    )
}