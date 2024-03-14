import { DAY_WORK_INTERVAL, TIME_STEP } from "../../constants/constanst"
import { Time } from "../../models/time"
import { TimeInterval } from "../../models/timeInterval"
import CSS from 'csstype'

import './style.css'
import { useState } from "react"
import { PeriodCreateDTO, PeriodModel, PeriodSchema, PeriodViewModel } from "../../models/period"
import { BiPlusCircle } from "react-icons/bi"
import { ModalInput } from "../Modal/ModalInput/ModalInput"
import { useSearchParams } from "react-router-dom"
import { PeriodEditorModal } from "./Modal/PeriodEditorModal"
import { PeriodItem } from "./PeriodItem/PeriodItem"
import usePeriods from "../../hooks/usePeriods"

interface DayTimelineProps {
    title: string
    periods: PeriodModel[]
    isEditable: boolean

    onAdd?: (period: PeriodSchema) => void
    onRemove?: (periodId: string) => void
    onUpdate?: (period: PeriodSchema) => void

    routineId?: string
    dayOfWeek?: number

    workPeriod?: TimeInterval
    timelineTimeStep?: Time
    periodTimeStep?: Time
}

export const PeriodsTimeline = ({
    workPeriod = DAY_WORK_INTERVAL,
    periodTimeStep = TIME_STEP,
    timelineTimeStep = Time.createFromHoursAndMinutes(1, 0),
    title,
    periods,
    isEditable,
    routineId,
    dayOfWeek
}: DayTimelineProps)  => {
    const [isInputOpen, setIsInputOpen] = useState(false)
    const {createPeriod, updatePeriod, isCollided} = usePeriods()

    const [searchParams] = useSearchParams()
    const periodId = searchParams.get("periodId")
    const openedPeriod = periods.find(p => p.id === periodId)

    const timelineDivisions = getTimelineDivisions(workPeriod, timelineTimeStep)
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
                    {title}
                </span>
                {
                    isEditable &&
                    <BiPlusCircle onClick={openModalInput}/>
                }
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
                            isEditable={isEditable || period.routineId == routineId}
                            period={periodModelToViewModel(period)}
                            step={step}
                            onChange={handlePeriodUpdating}
                            onSave={period => updatePeriod(periodViewModelToModel(period))}
                        />
                        )}
                </div>
            </div>
            {
                isInputOpen &&
                <ModalInput
                        placeholder='Введите название периода'
                        onClose={() => setIsInputOpen(false)}
                        onChange={handleAddingPeriod}
                />
            }
            {
                periodId && openedPeriod !== undefined &&
                <PeriodEditorModal
                    periodId={periodId}
                    //onClose={fetchPeriods}
                />
            }
        </div>
    )

    function handlePeriodUpdating(periodView: PeriodViewModel): boolean{
        const period = periodViewModelToModel(periodView)

        return !isCollided(period)
    }

    function handleAddingPeriod(periodName: string) {
        setIsInputOpen(false)

        if(routineId == undefined || dayOfWeek == undefined)
            throw 'RoutineId and DayOfWeek cannot to be empty'

        const dto: PeriodCreateDTO = {
            name: periodName,
            routineId: routineId,
            dayOfWeek: dayOfWeek
        }
        createPeriod(dto)
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

    function periodModelToViewModel(period: PeriodModel): PeriodViewModel{
        const view = {
            ...period,
            rowStart: timeToRow(period.timeInterval.startTime), 
            rowEnd: timeToRow(period.timeInterval.endTime)
        }
        console.log(`period: ${period.timeInterval} view: ${view.rowStart, view.rowEnd}`)
        console.log(period.timeInterval)
        console.log(`view:`)
        console.log(view.rowStart, view.rowEnd)
        return {
            ...period,
            rowStart: timeToRow(period.timeInterval.startTime), 
            rowEnd: timeToRow(period.timeInterval.endTime)
        }
    }

    function periodViewModelToModel(period: PeriodViewModel):PeriodModel{
        return new PeriodModel(
            period.id,
            period.name,
            period.routineId,
            period.dayOfWeek,
            new TimeInterval(rowToTime(period.rowStart), rowToTime(period.rowEnd))
        )
    }

    function timeToRow(time: Time): number{
        return Math.floor((time.totalMinutes-DAY_WORK_INTERVAL.startTime.totalMinutes+TIME_STEP.totalMinutes)/TIME_STEP.totalMinutes)
    }

    function rowToTime(row: number): Time{
        return new Time(DAY_WORK_INTERVAL.startTime.totalMinutes+(row* TIME_STEP.totalMinutes) -TIME_STEP.totalMinutes)
    }
}
