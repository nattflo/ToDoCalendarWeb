import { useParams } from "react-router-dom"
import { WORK_DAYS } from "../../constants/constanst"
import { PeriodEditor } from "../PeriodEditor/PeriodEditor"
import { Swiper } from "../Swiper/Swiper"
import { useEffect, useState } from "react"
import { Routine, RoutineSchema } from "../../models/routine"
import { httpGet } from "../../utils/http"

export const RoutineEditor = () => {

    const {id} = useParams()
    const [routine, setRoutine] = useState<Routine>()

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
                            return (
                                <PeriodEditor key={index} title={day} dayOfWeek={index} routineId={id}/>
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