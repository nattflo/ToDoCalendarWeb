import { createContext, useState, useEffect, PropsWithChildren, SetStateAction, Dispatch } from 'react';
import { httpGet } from '../utils/http';
import { Routine, RoutineSchema } from '../models/routine';

export interface RoutinesContextValue {
    routines: Routine[];
    setRoutines: Dispatch<SetStateAction<Routine[]>>;
  }

const RoutinesContext = createContext<RoutinesContextValue | null>(null)

const RoutinesProvider = ({ children }: PropsWithChildren) => {
    const [routines, setRoutines] = useState<Routine[]>([]);

    useEffect(() => {
        fetchPeriods()
    }, [])

    return (
        <RoutinesContext.Provider value={{ routines, setRoutines }}>
            {children}
        </RoutinesContext.Provider>
    )

    async function fetchPeriods() {
        const routineSchemas = await httpGet<RoutineSchema[]>('routines')
        const routines = routineSchemas.map(schema => Routine.createFromSchema(schema))
        setRoutines(routines);
    }
}

export { RoutinesContext, RoutinesProvider };