import { WORK_DAYS } from "../../constants/time"
import { DayTimeline } from "../DayTimeline/DayTimeline"
import './style.css'

export const WeekTable = () => {

    return(
        <div className="WeekTable">
            {
                WORK_DAYS.map(day => 
                    <DayTimeline key={day}/>   
                )
            }
        </div>
    )
}