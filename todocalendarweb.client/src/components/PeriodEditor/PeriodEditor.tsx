import { DAY_WORK_INTERVAL, TIME_STEP } from "../../constants/constanst";
import { Time } from "../../models/time"
import { TimeInterval } from "../../models/timeInterval";
import { PeriodItem } from "./PeriodItem/PeriodItem";
import CSS from 'csstype';

import './style.css';
import { useEffect, useState } from "react";
import { Period, PeriodSchema } from "../../models/period";
import { BiPlusCircle } from "react-icons/bi";
import { createPortal } from "react-dom";
import { ModalInput } from "../Modal/ModalInput/ModalInput";
import { httpGet, httpPost, httpPut } from "../../utils/http";

interface DayTimelineProps {
    workPeriod?: TimeInterval;
    timelineTimeStep?: Time;
    periodTimeStep?: Time;
    routineId: string;
}

export const PeriodEditor = ({
    workPeriod = DAY_WORK_INTERVAL,
    periodTimeStep = TIME_STEP,
    timelineTimeStep = Time.createFromHoursAndMinutes(1, 0),
    routineId
}: DayTimelineProps)  => {
    const [isInputOpen, setIsInputOpen] = useState(false);
    const [periods, setPeriods] = useState<Array<Period>>([]);

    //const [searchParams] = useSearchParams();
    //const periodId = searchParams.get("periodId");
    //const openedPeriod = periods.find(p => p.id === periodId);

    useEffect(() => {
        fetchPeriods();
    }, [])

    const updatePeriod = (changedPeriod: Period) => {
        httpPut<PeriodSchema>('periods', changedPeriod.id, changedPeriod.schema);
        const changedTasks = periods?.map(period => {
            if(period.id === changedPeriod.id) return changedPeriod;
            else return period;
        })
        setPeriods(changedTasks);
    }

    const getFreeTimePeriod = (): TimeInterval|undefined => {
        if(periods.length == 0){
            return new TimeInterval(workPeriod.startTime, new Time(workPeriod.startTime.totalMinutes + periodTimeStep.totalMinutes))
        }
        else{
            const timeSortedPeriods = periods.sort((a, b) => a.timeInterval.startTime.totalMinutes - b.timeInterval.startTime.totalMinutes)
            if(periods.length >= 2){
                for(let i = 1; i < timeSortedPeriods.length; i++) {
                    const prevPeriodEndTime = timeSortedPeriods[i-1].timeInterval.endTime.totalMinutes;
                    const currentPeriodStartTime = timeSortedPeriods[i].timeInterval.startTime.totalMinutes;
                    if(currentPeriodStartTime - prevPeriodEndTime >= periodTimeStep.totalMinutes){
                        return new TimeInterval(timeSortedPeriods[i-1].timeInterval.endTime, new Time(timeSortedPeriods[i-1].timeInterval.endTime.totalMinutes+periodTimeStep.totalMinutes))
                    }
                }
            }
            if(timeSortedPeriods[0].timeInterval.startTime.totalMinutes - workPeriod.startTime.totalMinutes >= periodTimeStep.totalMinutes){
                return new TimeInterval(workPeriod.startTime, timeSortedPeriods[0].timeInterval.startTime);
            }
            if(workPeriod.endTime.totalMinutes - timeSortedPeriods[timeSortedPeriods.length-1].timeInterval.endTime.totalMinutes >= periodTimeStep.totalMinutes){
                const startTime = timeSortedPeriods[timeSortedPeriods.length-1].timeInterval.endTime
                return new TimeInterval(startTime, new Time(startTime.totalMinutes+periodTimeStep.totalMinutes))
            }
        }
        return undefined;
    }

    const timelineDivisions = getTimelineDivisions(workPeriod, timelineTimeStep);

    const openModalInput = () => {
        setIsInputOpen(true)
    }

    const timeSize = 60;
    const step = 56/(Math.floor(timeSize/periodTimeStep.totalMinutes));
    
    const periodWrapperStyle: CSS.Properties = {
        gridTemplateRows: `repeat(${(workPeriod.endTime.totalMinutes - workPeriod.startTime.totalMinutes)/periodTimeStep.totalMinutes},${step}px)`
    }
    const timeStyle: CSS.Properties = {
        height: `${timeSize}px`
    }

    return(
        <div className='PeriodEditor'>
            <div className="PeriodEditorHeader">
                <span className="PeriodTitle">
                    Понедельник
                </span>
                <BiPlusCircle onClick={openModalInput}/>
            </div>
            <div className="DayTimeline">
                <div className="Timeline">
                    {timelineDivisions.map(time => 
                        <div key={`time-${time}`} className="Time" style={{...timeStyle}}>{time.toString()}</div>
                    )}
                </div>
                <div className="PeriodWrapper" style={{...periodWrapperStyle}}>
                    {periods.map((period) =>
                        <PeriodItem
                            key={period.id}
                            period={period}
                            id={period.id}
                            step={step}
                            interval={period.timeInterval}
                            occupiedIntevals={periods.filter(p => p.id !== period.id).map(period => period.timeInterval)}
                            onChange={updatePeriod}
                        />
                        )}
                </div>
            </div>
            {
                isInputOpen &&
                createPortal(
                    <ModalInput
                        placeholder='Введите название периода'
                        onClose={() => setIsInputOpen(false)}
                        onChange={(periodName) => addPeriod(periodName, 1)}
                    />, document.body)
            }
            {/* {
                periodId && openedPeriod !== undefined &&
                createPortal(<PeriodEditorModal period={openedPeriod} onChange={handlePeriodChange}/>, document.body)
            } */}
        </div>
    )

    async function fetchPeriods(){
        const periodSchemas = await httpGet<Array<PeriodSchema>>('routines/06e6d9cb-8e1e-4640-b4c9-2acd4048e86d/periods')
        const periods = periodSchemas.map(schema => Period.createFromSchema(schema));
        setPeriods(periods);
    }

    async function addPeriod (name: string, dayOfWeek: number) {
        setIsInputOpen(false);
        const freeTimeInterval = getFreeTimePeriod();
        if(freeTimeInterval != undefined){
            const periodSchema = {name: name, routineId: routineId, timePeriod: freeTimeInterval.schema, dayOfWeek: dayOfWeek}
            const schema = await httpPost<PeriodSchema, PeriodSchema>('periods', periodSchema);
            const period = Period.createFromSchema(schema);
            const newPeriods : Array<Period> = periods.length != 0 ? [...periods, period] : [period];
            setPeriods(newPeriods);
        }
    }
}

function getTimelineDivisions (
    period: TimeInterval,
    timeStep: Time): Array<Time>  {

    const timelineDivisions = new Array<Time>();
    
    for (let time = period.startTime; 
        time.totalMinutes <= period.endTime.totalMinutes; 
        time = time.addMinutes(timeStep.totalMinutes)) {

        timelineDivisions.push(time);
        
    }

    return timelineDivisions;

}