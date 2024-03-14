import { createContext, useState, useEffect, PropsWithChildren, SetStateAction, Dispatch } from 'react';
import { PeriodModel, PeriodSchema } from '../models/period';
import { httpGet } from '../utils/http';

export interface PeriodsContextValue {
    periods: PeriodModel[];
    setPeriods: Dispatch<SetStateAction<PeriodModel[]>>;
  }

const PeriodsContext = createContext<PeriodsContextValue | null>(null)

const PeriodsProvider = ({ children }: PropsWithChildren) => {
    const [periods, setPeriods] = useState<PeriodModel[]>([]);

    useEffect(() => {
        fetchPeriods()
    }, [])

    return (
        <PeriodsContext.Provider value={{ periods, setPeriods }}>
            {children}
        </PeriodsContext.Provider>
    )

    async function fetchPeriods() {
        const periodSchemas = await httpGet<PeriodSchema[]>('periods')
        const periods = periodSchemas.map(schema => PeriodModel.createFromSchema(schema))
        setPeriods(periods);
    }
}

export { PeriodsContext, PeriodsProvider };