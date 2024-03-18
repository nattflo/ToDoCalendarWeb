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
        console.log('before set periods')
        
        setCurrentPeriods(filteredPeriods)

        console.log('after set periods')

        const time = Time.createFromHoursAndMinutes(currentTime.getHours(), currentTime.getMinutes())
        const period = periods.find(p => p.timeInterval.startTime.totalMinutes <= time.totalMinutes && p.timeInterval.endTime.totalMinutes > time.totalMinutes)
        
        period != null && setCurrentPeriod(period)

        console.log('after set period')
        console.log('________________')
        

    }, [periods])

    console.log(currentPeriod)
    return (
        <div className="CurrentDay">
            <h2 className="Title">
                Просмотр текущего дня
            </h2>
            <div className="Content">
                <PeriodsTimeline
                    title="Сегодня"
                    periods={currentPeriods}
                    isEditable={false}
                />
                {
                    currentPeriod != null &&
                    <div className="Information">
                        <div className="PeriodInformationWrapper">
                            <h3 className="PeriodHeader">
                                Текущий период:
                            </h3>
                            <div className="PeriodInformation">
                                <b>{currentPeriod.name}</b>
                                <b className="timeProgressBar">
                                    {currentPeriod.timeInterval.startTime.toString()}
                                    -
                                    {currentPeriod.timeInterval.endTime.toString()}
                                </b>
                            </div>
                        </div>
                        <div className="PeriodTasks">
                            <h3 className="PeriodTasksHeader">
                                Задачи:
                            </h3>
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