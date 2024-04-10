import { BiTrashAlt } from "react-icons/bi"
import './style.css'


interface TrashCanIconProps {
    onClick?: () => void
}

export const TrashCanIcon = ({
    onClick= () => {}
    }: TrashCanIconProps) => {
    return (
        <BiTrashAlt
            className="TrashCanButton"
            onClick={onClick}
        />
    )
}