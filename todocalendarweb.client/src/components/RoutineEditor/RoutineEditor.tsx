import { useParams } from "react-router-dom"
import { WORK_DAYS } from "../../constants/constanst"
import { Swiper } from "../Swiper/Swiper"
import { useEffect, useState } from "react"
import { Routine } from "../../models/routine"
import { PeriodsTimeline } from "../PeriodsTimeline/PeriodsTimeline"
import usePeriods from "../../hooks/usePeriods"
import useRoutines from "../../hooks/useRoutines"
import { PeriodModel } from "../../models/period"

import './style.css'

export const RoutineEditor = () => {

    const {id} = useParams()
    const [routine, setRoutine] = useState<Routine>()
    const [currentPeriods, setPeriods] = useState<PeriodModel[]>()
    const {routines} = useRoutines()
    const {periods} = usePeriods()

    useEffect(() => {
        if(routines.length > 0)
            setRoutine(routines.find(r => r.id == id))
        if(periods.length > 0)
            setPeriods(periods)
    }, [id, routines, periods])

    return (
        <div className="RoutineEditor">
            {
                routine != undefined &&
                <h1 className="RoutineHeader">{routine.name}</h1>
            }
            {
                id != undefined && 
                currentPeriods != undefined &&
                <Swiper slidesPerView={4}>
                    {
                        WORK_DAYS.map((day, index) => {
                            const filteredPeriods = currentPeriods.filter(period => period.dayOfWeek == index)
                            return (
                                <PeriodsTimeline 
                                    key={index}
                                    title={day}
                                    dayOfWeek={index}
                                    routineId={id}
                                    periods={filteredPeriods}
                                    isEditable={true}
                                />
                            )
                        })
                    }
                </Swiper>
            }
        </div>
    )
}