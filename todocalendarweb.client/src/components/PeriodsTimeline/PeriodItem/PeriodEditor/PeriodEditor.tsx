import Draggable from "react-draggable"
import { useEffect, useRef, useState } from "react"
import { Link } from "react-router-dom"
import { PeriodViewModel } from "../../../../models/period"
import { TIME_STEPS_COUNT } from "../../../../constants/constanst"

type periodProps ={
    period: PeriodViewModel
    step: number
    onChange?: (period: PeriodViewModel) => boolean
    onSave?: (period: PeriodViewModel) => void
}


export const PeriodEditor = ({period, step, onChange = () => true, onSave = () => {} }: periodProps) => {

    const [isMoving, setIsMoving] = useState(false)
    const [isCollided, setIsCollided] = useState(false)
    const [originalPeriod, setoriginalPeriod] = useState<PeriodViewModel>(period)
    const [currentPeriod, setCurrentPeriod] = useState<PeriodViewModel>(period)

    const topResizeHandleRef = useRef(null)
    const bottomResizeHandleRef = useRef(null)
    const movingHandleRef = useRef(null)

    useEffect(() => {
        setCurrentPeriod(period)
        setoriginalPeriod(period)
    }, [period])

    const handleTopResize = (direction: number) => {

        const nextRowStart = currentPeriod.rowStart + direction

        if(nextRowStart >= 0 && nextRowStart < currentPeriod.rowEnd && nextRowStart != currentPeriod.rowStart)
            {
                const newPeriod: PeriodViewModel = {
                    ...currentPeriod,
                    rowStart: nextRowStart,
                }
                if(onChange(newPeriod)){
                    setCurrentPeriod(newPeriod)
                    setoriginalPeriod(newPeriod)
                }
            }
    }
    const handleMoving = (direction: number) => {

        const nextRowStart = currentPeriod.rowStart + direction
        const nextRowEnd = currentPeriod.rowEnd + direction

        if(nextRowStart >= 1 && nextRowEnd <= TIME_STEPS_COUNT+1){

            const newPeriod: PeriodViewModel = {
                ...currentPeriod,
                rowStart: nextRowStart,
                rowEnd: nextRowEnd
            }
            setCurrentPeriod(newPeriod)

            if(onChange(newPeriod)){
                setIsCollided(false)
                setCurrentPeriod(newPeriod)
            }else setIsCollided(true)
        }
    }
    const handleBottomResize = (direction: number) => {

        const nextRowEnd = currentPeriod.rowEnd + direction

        if(nextRowEnd != currentPeriod.rowEnd && nextRowEnd > currentPeriod.rowStart && nextRowEnd <= TIME_STEPS_COUNT){
                const newPeriod: PeriodViewModel = {
                    ...currentPeriod,
                    rowEnd: nextRowEnd
                }
                if(onChange(newPeriod)){
                    setCurrentPeriod(newPeriod)
                    setoriginalPeriod(newPeriod)
                }
            }
    }
    
    const applyMoving = () => {
        setIsMoving(false)
        if(isCollided){
            setIsCollided(false)
            setCurrentPeriod(originalPeriod)
            onSave(originalPeriod)
        }else onSave(currentPeriod)
    }

    const collidingStyle = isCollided ? {backgroundColor: 'red'} : {}
    const movingStyle = isMoving ? {opacity: '0.5', zIndex: '2'} : {}
    
    return(
        <div className='PeriodItemEditor' style={{gridRowStart: currentPeriod.rowStart, gridRowEnd: currentPeriod.rowEnd, ...collidingStyle, ...movingStyle}}>
            <Link
                to={`?periodId=${period.id}`}
                className="PeriodName"
            >
                {period.name}
            </Link>
            <Draggable
                position={{ x: 0, y: 0 }}
                defaultPosition={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => {handleTopResize(Math.floor(data.y / step))}}
                onStop={() => onSave(currentPeriod)}
                nodeRef={topResizeHandleRef}
            >
                <div ref={topResizeHandleRef} className="ResizeHandle" style={{top: '0px'}}/>
            </Draggable>
            <Draggable
                position={{ x: 0, y: 0 }}
                defaultPosition={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                scale={1}
                onDrag={(_, data) => handleMoving(Math.floor(data.y / step))}
                onStop={applyMoving}
                onStart={() => setIsMoving(true)}
                nodeRef={movingHandleRef}
            >
                <div ref={movingHandleRef} className="MoveHandle"/>
            </Draggable>
            <Draggable
                position={{ x: 0, y: 0 }}
                defaultPosition={{ x: 0, y: 0 }}
                axis='y'
                grid={[1,step]}
                onDrag={(_, data) => handleBottomResize(Math.floor(data.deltaY / step))}
                onStop={() => onSave(currentPeriod)}
                nodeRef={bottomResizeHandleRef}
            >
                <div ref={bottomResizeHandleRef} className="ResizeHandle" style={{bottom: '0px'}}/>
            </Draggable>
    </div>
    )
}