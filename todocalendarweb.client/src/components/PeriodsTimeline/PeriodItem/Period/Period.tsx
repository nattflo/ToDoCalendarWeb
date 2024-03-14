import { PeriodViewModel } from "../../../../models/period"

interface PeriodProps {
    period: PeriodViewModel
}

export const Period = ({
    period
}: PeriodProps) => {
    
    return (
        <div className="Period" style={{gridRowStart: period.rowStart, gridRowEnd: period.rowEnd}}>
            {period.name}
        </div>
    )
}