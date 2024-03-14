import { useParams } from "react-router-dom"
import { WORK_DAYS } from "../../constants/constanst"
import { Swiper } from "../Swiper/Swiper"
import { useEffect, useState } from "react"
import { Routine, RoutineSchema } from "../../models/routine"
import { httpGet } from "../../utils/http"
import { PeriodsTimeline } from "../PeriodsTimeline/PeriodsTimeline"
import usePeriods from "../../hooks/usePeriods"

export const RoutineEditor = () => {

    const {id} = useParams()
    const [routine, setRoutine] = useState<Routine>()
    const {periods} = usePeriods()

    useEffect(() => {
        if(id != undefined)
            fetchRoutine()
    }, [])

    return (
        <div className="RoutineEditor">
            {
                routine != undefined &&
                <h1>{routine.name}</h1>
            }
            {
                id != undefined && 
                <Swiper slidesPerView={4}>
                    {
                        WORK_DAYS.map((day, index) => {
                            const filteredPeriods = periods.filter(period => period.dayOfWeek == index)
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

    async function fetchRoutine() {
        const routineSchema = await httpGet<RoutineSchema>('routines/'+id)
        const routine = Routine.createFromSchema(routineSchema)
        setRoutine(routine)
    }
}