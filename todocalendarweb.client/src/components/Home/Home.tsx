import { useEffect, useState } from "react"
import usePeriods from "../../hooks/usePeriods"
import { PeriodsTimeline } from "../PeriodsTimeline/PeriodsTimeline"
import TaskWrapperModes, { TaskEditor } from "../TaskEditor/TaskEditor"
import { PeriodModel } from "../../models/period"
import { Time } from "../../models/time"

import './style.css'



export const Home = () => {

    const [currentPeriods, setCurrentPeriods] = useState<PeriodModel[]>([])
    const [currentPeriod, setCurrentPeriod] = useState<PeriodModel>()
    const {periods} = usePeriods()

    useEffect(() => {
        const currentTime = new Date()
        const dayOfWeek = [6,0,1,2,3,4,5][currentTime.getDay()]
        const filteredPeriods = periods.filter(p => p.dayOfWeek == dayOfWeek)
        
        setCurrentPeriods(filteredPeriods)

        const time = Time.createFromHoursAndMinutes(currentTime.getHours(), currentTime.getMinutes())
        const period = periods.find(p => p.timeInterval.startTime.totalMinutes <= time.totalMinutes && p.timeInterval.endTime.totalMinutes > time.totalMinutes)
        
        period != null && setCurrentPeriod(period)
        

    }, [periods])

    console.log(currentPeriod)
    return (
        <div className="CurrentDay">
            <h1 className="Title">
                Просмотр текущего дня
            </h1>
            <div className="Content">
                <PeriodsTimeline
                    title="Сегодня"
                    periods={currentPeriods}
                    isEditable={false}
                />
                {
                    currentPeriod != null &&
                    <div className="Information">
                        <h3 className="PeriodHeader">
                            Текущий период:
                        </h3>
                        <div className="PeriodInformation">
                            <b>{currentPeriod.name}</b>
                            <b className="TimeProgressBar">
                                {currentPeriod.timeInterval.startTime.toString()}
                                -
                                {currentPeriod.timeInterval.endTime.toString()}
                            </b>
                        </div>
                        <h3 className="PeriodTasksHeader">
                            Задачи:
                        </h3>
                        <div className="PeriodTasks">
                            <TaskEditor
                                mode={TaskWrapperModes.Executing}
                                periodId={currentPeriod?.id}
                            />
                        </div>
                    </div>
                }
            </div>
        </div>
    )
}