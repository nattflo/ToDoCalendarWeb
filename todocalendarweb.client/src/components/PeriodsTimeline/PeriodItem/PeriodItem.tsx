import { PeriodViewModel } from "../../../models/period"
import { Period } from "./Period/Period";
import { PeriodEditor } from "./PeriodEditor/PeriodEditor"

import './style.css';


interface PeriodItemProps{
    isEditable?: boolean
    period: PeriodViewModel
    step: number
    onChange?: (period: PeriodViewModel) => boolean
    onSave?: (period: PeriodViewModel) => void
}

export const PeriodItem = ({
    isEditable = false,
    period,
    step,
    onChange = () => true,
    onSave = () => {}
}: PeriodItemProps) => {

    return(
        <>
        {
            isEditable ?
                <PeriodEditor
                    period={period}
                    step={step}
                    onChange={onChange}
                    onSave={onSave}
                /> 
            :
                <Period
                    period={period}
                />
        }
        </>
    )
}