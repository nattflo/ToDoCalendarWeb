import { useContext } from 'react';
import { PeriodCreateDTO, PeriodModel, PeriodSchema } from '../models/period';
import { httpDelete, httpPost, httpPut } from '../utils/http';
import { DAY_WORK_INTERVAL, TIME_STEP } from '../constants/constanst';
import { Time } from '../models/time';
import { TimeInterval } from '../models/timeInterval';
import { PeriodsContext } from '../contexts/PeriodContext';

export default function usePeriods() {

	const context = useContext(PeriodsContext)

	if (!context) throw new Error('usePeriods must be used within a PeriodsProvider')
  
	const { periods, setPeriods } = context

	async function createPeriod(periodCreateDTO: PeriodCreateDTO) {
		const timePeriod = getFreeTimePeriod(periodCreateDTO.dayOfWeek)
		if(timePeriod != undefined){
			const periodSchema: PeriodSchema = {
				...periodCreateDTO,
				timePeriod: timePeriod.schema
			}
			const schema = await httpPost<PeriodSchema>('periods', periodSchema);
			const period = PeriodModel.createFromSchema(schema);
			setPeriods(prev => [...prev, period]);
		}
	}

	function updatePeriod(period: PeriodModel) {
		httpPut<PeriodSchema>('periods', period.id, period.schema)

		setPeriods(prev => prev.map(p => p.id === period.id ? period : p))
	}

	function deletePeriod(periodId: string) {
		httpDelete(`periods/${periodId}`)

		setPeriods(prev => 
		prev.filter(p => p.id !== periodId)
		);
	}

	function isCollided(period: PeriodModel): boolean {
		const otherPeriods = periods.filter(p => p.id != period.id && p.dayOfWeek == period.dayOfWeek)
		return otherPeriods.some(p => p.timeInterval.isCollideWithTimeInterval(period.timeInterval))
	}

	const getFreeTimePeriod = (dayOfWeek: number): TimeInterval|undefined => {

		const periodsByDay = periods.filter(p => p.dayOfWeek == dayOfWeek)

        if(periodsByDay.length == 0){
            return new TimeInterval(DAY_WORK_INTERVAL.startTime, new Time(DAY_WORK_INTERVAL.startTime.totalMinutes + TIME_STEP.totalMinutes))
        }
        else{
            const timeSortedPeriods = periodsByDay.sort((a, b) => a.timeInterval.startTime.totalMinutes - b.timeInterval.startTime.totalMinutes)
            if(periodsByDay.length >= 2){
                for(let i = 1; i < timeSortedPeriods.length; i++) {
                    const prevPeriodEndTime = timeSortedPeriods[i-1].timeInterval.endTime.totalMinutes;
                    const currentPeriodStartTime = timeSortedPeriods[i].timeInterval.startTime.totalMinutes;
                    if(currentPeriodStartTime - prevPeriodEndTime >= TIME_STEP.totalMinutes){
                        return new TimeInterval(timeSortedPeriods[i-1].timeInterval.endTime, new Time(timeSortedPeriods[i-1].timeInterval.endTime.totalMinutes+TIME_STEP.totalMinutes))
                    }
                }
            }
            if(timeSortedPeriods[0].timeInterval.startTime.totalMinutes - DAY_WORK_INTERVAL.startTime.totalMinutes >= TIME_STEP.totalMinutes){
                return new TimeInterval(DAY_WORK_INTERVAL.startTime, timeSortedPeriods[0].timeInterval.startTime);
            }
            if(DAY_WORK_INTERVAL.endTime.totalMinutes - timeSortedPeriods[timeSortedPeriods.length-1].timeInterval.endTime.totalMinutes >= TIME_STEP.totalMinutes){
                const startTime = timeSortedPeriods[timeSortedPeriods.length-1].timeInterval.endTime
                return new TimeInterval(startTime, new Time(startTime.totalMinutes+TIME_STEP.totalMinutes))
            }
        }
        return undefined;
    }

	return {
		periods,
		createPeriod,
		updatePeriod,
		deletePeriod,
		isCollided
	};
}