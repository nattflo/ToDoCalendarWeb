import { KeyboardEvent, useState, FocusEvent } from "react";

interface EditableTextProps {
    tag: keyof JSX.IntrinsicElements;
    text: string;
    onChange?: (text: string) => void;
}

export const EditableText = ({tag : Wrapper, text, onChange = () => {}}: EditableTextProps) => {

    const [isEdit, setIsEdit] = useState(false);
    const [currentText, setCurrentText] = useState(text);

    const handleKeyDown = (event: KeyboardEvent) => {
        let element = (event.currentTarget as HTMLElement);
        if (event.key === 'Enter'){
            
            element.blur();
            event.preventDefault();
          }
    }
    const handleBlur = (event: FocusEvent) => {
        let element = (event.currentTarget as HTMLElement);
        applyText(element);
    }
    const applyText = (element: HTMLElement) => {
        let input = element.innerText;

        setIsEdit(false)
        if(input.length > 0){

            setCurrentText(input);
            onChange(input);
        }else element.innerText = currentText;

    }
    
    const style = isEdit ?  {
    fontStyle: 'italic'
    } : {};

    return (
        <Wrapper
            style={style}
            suppressContentEditableWarning={true}
            contentEditable
            onKeyDown={handleKeyDown}
            onClick={() => setIsEdit(true)}
            onBlur={handleBlur}
        >
            {currentText}
        </Wrapper> 
    )
}