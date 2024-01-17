import { useState } from "react";
import { Period } from "../../../models/period"
import { EditableText } from "../../EditableText/EditableText"
import { Modal } from "../modal"

interface PeriodEditorModalProps {
    period: Period,
    onChange? : (period : Period) => void;
}
export const PeriodEditorModal = ({period, onChange = () => {}}: PeriodEditorModalProps) => {
    
    const [currentPeriod, setPeriod] = useState(period);

    const handleChange = (period: Period) => {
        //отправить на бэк, получить и заменить
        setPeriod(period);
        onChange(period);
    }

    return (
        <Modal>
            <EditableText tag='b' text={currentPeriod.name} onChange={name => handleChange(new Period(period.id, name, period.timeInterval))}/>
        </Modal>
    )
}