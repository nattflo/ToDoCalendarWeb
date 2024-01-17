import Draggable from "react-draggable";
import './style.css';
import { TimeInterval } from "../../../models/timeInterval";
import { useState } from "react";
import { DAY_WORK_INTERVAL, TIME_STEP } from "../../../constants/time";
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


export const PeriodView = ({period, step, occupiedIntevals, onChange = () => {} }: periodProps) => {

    const [isMoving, setIsMoving] = useState(false);
    const [isCollided, setIsCollided] = useState(false);
    const [lastAllowedInterval, setLastAllowedInterval] = useState<TimeInterval>(period.timeInterval);
    const [currentInterval, setCurrentInterval] = useState<TimeInterval>(period.timeInterval);

    const handleTopResize = (direction: number) => {

        let nextTime = currentInterval.startTime.addMinutes(direction * TIME_STEP.totalMinutes);
        let nextInterval = new TimeInterval(nextTime, currentInterval.endTime);
        if(nextTime.totalMinutes < currentInterval.endTime.totalMinutes &&
            DAY_WORK_INTERVAL.isIncludesTimeStictly(nextTime) && 
            !occupiedIntevals.some(interval => interval.isCollideWithTimeInterval(nextInterval))){
                setCurrentInterval(nextInterval);
                setLastAllowedInterval(nextInterval);
                onChange(new Period(period.id, period.name, nextInterval));
            }
    }
    const handleMoving = (direction: number) => {

        let nextStartTime = currentInterval.startTime.addMinutes(direction * TIME_STEP.totalMinutes);
        let nextEndTime = currentInterval.endTime.addMinutes(direction * TIME_STEP.totalMinutes);
        let nextInterval = new TimeInterval(nextStartTime, nextEndTime);

        if(DAY_WORK_INTERVAL.isIncludesTimeStictly(nextStartTime) && 
            DAY_WORK_INTERVAL.isIncludesTimeStictly(nextEndTime)){

            setCurrentInterval(nextInterval);

            if(!occupiedIntevals.some(interval => interval.isCollideWithTimeInterval(nextInterval)) &&
                !occupiedIntevals.some(interval => nextInterval.isCollideWithTimeInterval(interval))&&
                !occupiedIntevals.some(interval => interval.isEqual(nextInterval))){
                setIsCollided(false);
                setLastAllowedInterval(nextInterval);
                onChange(new Period(period.id, period.name, nextInterval));
            }else setIsCollided(true);
        }
    }
    const handleBottomResize = (direction: number) => {

        let nextTime = currentInterval.endTime.addMinutes(direction * TIME_STEP.totalMinutes);
        let nextInterval = new TimeInterval(currentInterval.startTime, nextTime);

        if(nextTime.totalMinutes > currentInterval.startTime.totalMinutes &&
            DAY_WORK_INTERVAL.isIncludesTimeStictly(nextTime) && 
            !occupiedIntevals.some(interval => interval.isCollideWithTimeInterval(nextInterval))){
                setCurrentInterval(nextInterval);
                setLastAllowedInterval(nextInterval);
                onChange(new Period(period.id, period.name, nextInterval));
            }
    }
    
    const applyMoving = () => {
        setIsMoving(false);
        if(isCollided){
            setIsCollided(false);
            setCurrentInterval(lastAllowedInterval);
        }
    }

    const rowStart = Math.floor((currentInterval.startTime.totalMinutes-DAY_WORK_INTERVAL.startTime.totalMinutes+TIME_STEP.totalMinutes)/TIME_STEP.minutes);
    const rowEnd = Math.floor((currentInterval.endTime.totalMinutes-DAY_WORK_INTERVAL.startTime.totalMinutes+TIME_STEP.totalMinutes)/TIME_STEP.minutes);
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
            >
                <div className="ResizeHandle" style={{top: '0px'}}/>
            </Draggable>
            <Draggable
                position={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => handleMoving(data.y/step)}
                onStop={applyMoving}
                onStart={() => setIsMoving(true)}
            >
                <div className="MoveHandle"/>
            </Draggable>
            <Draggable
                position={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => handleBottomResize(data.deltaY/step)}
            >
                <div className="ResizeHandle" style={{bottom: '0px'}}/>
            </Draggable>
    </div>
    )
}