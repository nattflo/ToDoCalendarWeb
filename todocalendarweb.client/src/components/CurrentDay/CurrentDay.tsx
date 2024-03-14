import usePeriods from "../../hooks/usePeriods"
import { PeriodsTimeline } from "../PeriodsTimeline/PeriodsTimeline"



export const CurrentDay = () => {

    const {periods} = usePeriods()
    const dayOfWeek = [6,0,1,2,3,4,5][new Date().getDay()]
    const filteredPeriods = periods.filter(p => p.dayOfWeek == dayOfWeek)
    return (
        <div className="CurrentDay">
            <PeriodsTimeline
                title="Сегодня"
                periods={filteredPeriods}
                isEditable={false}
            />
        </div>
    )
}