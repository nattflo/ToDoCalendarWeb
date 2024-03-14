import { useContext } from "react"
import { Routine, RoutineCreateDTO, RoutineSchema } from "../models/routine"
import { httpDelete, httpPost, httpPut } from "../utils/http"
import { RoutinesContext } from "../contexts/RoutineContext"

export default function useRoutines() {
    const context = useContext(RoutinesContext)

	if (!context) throw new Error('usePeriods must be used within a PeriodsProvider')
  
	const { routines, setRoutines } = context

    async function createRoutine(routineCreateDTO: RoutineCreateDTO) {
        const routineSchema: RoutineSchema = {
            ...routineCreateDTO,
            periods: []
        }
        const schema = await httpPost<RoutineSchema>('routines', routineSchema)
        const routine = Routine.createFromSchema(schema)
        setRoutines(prev => [...prev, routine])
	}

    function updateRoutine(routine: Routine) {
        httpPut<RoutineSchema>('routines', routine.id, routine.schema)
        setRoutines(prev => 
        prev.map(r => r.id === routine.id ? routine : r))
    }

    function deleteRoutine(routineId: string) {
		httpDelete(`routines/${routineId}`)
		setRoutines(prev => 
		prev.filter(r => r.id !== routineId)
		)
	}

    return {
		routines,
		createRoutine,
		updateRoutine,
		deleteRoutine
	}
}