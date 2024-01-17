import { DAY_WORK_INTERVAL, TIME_STEP } from "../../constants/time";
import { Time } from "../../models/time"
import { TimeInterval } from "../../models/timeInterval";
import { PeriodView } from "./PeriodView/PeriodView";
import CSS from 'csstype';

import './style.css';
import React, { useState } from "react";
import { Period } from "../../models/period";
import { useSearchParams } from "react-router-dom";
import { createPortal } from "react-dom";
import { PeriodEditorModal } from "../Modal/PeriodEditorModal/PeriodEditorModal";

interface DayTimelineProps
    extends React.HTMLAttributes<HTMLDivElement> {
    workPeriod?: TimeInterval;
    timelineTimeStep?: Time;
    periodTimeStep?: Time;
}

export const DayTimeline = ({
    workPeriod = DAY_WORK_INTERVAL,
    periodTimeStep = TIME_STEP,
    timelineTimeStep = Time.createFromHoursAndMinutes(1, 0), ...other}: DayTimelineProps)  => {
    const MOCKperiods = [
        new Period('0', 'Уборка',new TimeInterval(Time.createFromTimeSpanFormat('00:10:00'), Time.createFromTimeSpanFormat('00:12:00'))),
        new Period('1', 'Готовка',new TimeInterval(Time.createFromTimeSpanFormat('00:14:00'), Time.createFromTimeSpanFormat('00:16:00'))),
    ]
    const [periods, setPeriods] = useState<Array<Period>>(MOCKperiods);
    const [searchParams] = useSearchParams();
    const periodId = searchParams.get("periodId");
    const openedPeriod = periods.find(p => p.id === periodId);
    const timelineDivisions = getTimelineDivisions(workPeriod, timelineTimeStep);

    const handlePeriodChange = (period: Period) => {
        const changedPeriodIndex = periods.findIndex(p => p.id === period.id);
        const newPeriods = [...periods];
        newPeriods[changedPeriodIndex] = period;
        setPeriods(newPeriods);

    }
    const timeSize = 60;
    const step = 56/(Math.floor(60/periodTimeStep.totalMinutes));
    
    const periodWrapperStyle: CSS.Properties = {
        gridTemplateRows: `repeat(${(workPeriod.endTime.totalMinutes - workPeriod.startTime.totalMinutes)/periodTimeStep.totalMinutes},${step}px)`
    }
    const timeStyle: CSS.Properties = {
        height: `${timeSize}px`
    }

    return(
        <div className="DayTimeline" {...other}>
            <div className="Timeline">
                {timelineDivisions.map(time => 
                    <div key={`time-${time}`} className="Time" style={{...timeStyle}}>{time.toString()}</div>
                )}
            </div>
            <div className="PeriodWrapper" style={{...periodWrapperStyle}}>
                {periods.map((period) =>
                    <PeriodView
                        key= {period.id}
                        period={period}
                        id = {period.id}
                        step = {step}
                        interval={period.timeInterval}
                        occupiedIntevals={periods.filter(p => p.id !== period.id).map(period => period.timeInterval)}
                        onChange={handlePeriodChange}
                    />
                    )}
            </div>
            {
                periodId && openedPeriod !== undefined &&
                createPortal(<PeriodEditorModal period={openedPeriod} onChange={handlePeriodChange}/>, document.body)
            }
        </div>
    )
}

function getTimelineDivisions (
    period: TimeInterval,
    timeStep: Time): Array<Time>  {

    let timelineDivisions = new Array<Time>();
    
    for (let time = period.startTime; 
        time.totalMinutes <= period.endTime.totalMinutes; 
        time = time.addMinutes(timeStep.totalMinutes)) {

        timelineDivisions.push(time);
        
    }

    return timelineDivisions;

}