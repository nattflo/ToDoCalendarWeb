import Draggable from "react-draggable";
import './style.css';
import { TimeInterval } from "../../../models/timeInterval";
import { useRef, useState } from "react";
import { DAY_WORK_INTERVAL, TIME_STEP } from "../../../constants/constanst";
import { Link } from "react-router-dom";
import { Period } from "../../../models/period";

type periodProps ={
    id: string,
    period: Period;
    step: number;
    interval: TimeInterval;
    occupiedIntevals: Array<TimeInterval>;
    onChange?: (period: Period) => void;
}


export const PeriodItem = ({period, step, occupiedIntevals, onChange = () => {} }: periodProps) => {

    const [isMoving, setIsMoving] = useState(false);
    const [isCollided, setIsCollided] = useState(false);
    const [lastAllowedInterval, setLastAllowedInterval] = useState<TimeInterval>(period.timeInterval);
    const [currentPeriod, setCurrentPeriod] = useState<Period>(period);

    const topResizeHandleRef = useRef(null);
    const bottomResizeHandleRef = useRef(null);
    const movingHandleRef = useRef(null);

    const handleTopResize = (direction: number) => {

        const nextTime = currentPeriod.timeInterval.startTime.addMinutes(direction * TIME_STEP.totalMinutes);
        const nextInterval = new TimeInterval(nextTime, currentPeriod.timeInterval.endTime);
        if(nextTime.totalMinutes < currentPeriod.timeInterval.endTime.totalMinutes &&
            DAY_WORK_INTERVAL.isIncludesTimeStictly(nextTime) && 
            !occupiedIntevals.some(interval => interval.isCollideWithTimeInterval(nextInterval))){
                setCurrentPeriod(new Period(period.id, period.name, period.routineId, period.dayOfWeek, nextInterval));
                setLastAllowedInterval(nextInterval);
            }
    }
    const handleMoving = (direction: number) => {

        const nextStartTime = currentPeriod.timeInterval.startTime.addMinutes(direction * TIME_STEP.totalMinutes);
        const nextEndTime = currentPeriod.timeInterval.endTime.addMinutes(direction * TIME_STEP.totalMinutes);
        const nextInterval = new TimeInterval(nextStartTime, nextEndTime);

        if(DAY_WORK_INTERVAL.isIncludesTimeStictly(nextStartTime) && 
            DAY_WORK_INTERVAL.isIncludesTimeStictly(nextEndTime)){

                setCurrentPeriod(new Period(period.id, period.name, period.routineId, period.dayOfWeek, nextInterval));

            if(!occupiedIntevals.some(interval => interval.isCollideWithTimeInterval(nextInterval)) &&
                !occupiedIntevals.some(interval => nextInterval.isCollideWithTimeInterval(interval))&&
                !occupiedIntevals.some(interval => interval.isEqual(nextInterval))){
                setIsCollided(false);
                setLastAllowedInterval(nextInterval);
            }else setIsCollided(true);
        }
    }
    const handleBottomResize = (direction: number) => {

        const nextTime = currentPeriod.timeInterval.endTime.addMinutes(direction * TIME_STEP.totalMinutes);
        const nextInterval = new TimeInterval(currentPeriod.timeInterval.startTime, nextTime);

        if(nextTime.totalMinutes > currentPeriod.timeInterval.startTime.totalMinutes &&
            DAY_WORK_INTERVAL.isIncludesTimeStictly(nextTime) && 
            !occupiedIntevals.some(interval => interval.isCollideWithTimeInterval(nextInterval))){
                setCurrentPeriod(new Period(period.id, period.name, period.routineId, period.dayOfWeek, nextInterval));
                setLastAllowedInterval(nextInterval);
            }
    }
    
    const applyMoving = () => {
        setIsMoving(false);
        if(isCollided){
            setIsCollided(false);
            setCurrentPeriod(new Period(period.id, period.name, period.routineId, period.dayOfWeek, lastAllowedInterval));
        }else onChange(currentPeriod);
    }

    const rowStart = Math.floor((currentPeriod.timeInterval.startTime.totalMinutes-DAY_WORK_INTERVAL.startTime.totalMinutes+TIME_STEP.totalMinutes)/TIME_STEP.minutes);
    const rowEnd = Math.floor((currentPeriod.timeInterval.endTime.totalMinutes-DAY_WORK_INTERVAL.startTime.totalMinutes+TIME_STEP.totalMinutes)/TIME_STEP.minutes);
    const collidingStyle = isCollided ? {backgroundColor: 'red'} : {}
    const movingStyle = isMoving ? {opacity: '0.5', zIndex: '2'} : {}
    
    return(
        <div className='Period' style={{gridRowStart: `${rowStart}`, gridRowEnd: `${rowEnd}`, ...collidingStyle, ...movingStyle}}>
            <Link
                to={`?periodId=${period.id}`}
                className="PeriodName"
            >
                {period.name}
            </Link>
            <Draggable
                position={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => handleTopResize(data.y/step)}
                onStop={() => onChange(currentPeriod)}
                nodeRef={topResizeHandleRef}
            >
                <div ref={topResizeHandleRef} className="ResizeHandle" style={{top: '0px'}}/>
            </Draggable>
            <Draggable
                position={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => handleMoving(data.y/step)}
                onStop={applyMoving}
                onStart={() => setIsMoving(true)}
                nodeRef={movingHandleRef}
            >
                <div ref={movingHandleRef} className="MoveHandle"/>
            </Draggable>
            <Draggable
                position={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => handleBottomResize(data.deltaY/step)}
                onStop={() => onChange(currentPeriod)}
                nodeRef={bottomResizeHandleRef}
            >
                <div ref={bottomResizeHandleRef} className="ResizeHandle" style={{bottom: '0px'}}/>
            </Draggable>
    </div>
    )
}