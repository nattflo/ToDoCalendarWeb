import { useEffect, useState } from "react"
import { httpDelete, httpGet, httpPost, httpPut } from "../../utils/http"
import { Routine, RoutineSchema } from "../../models/routine"
import { BiPlusCircle, BiTrashAlt } from "react-icons/bi"

import './style.css'
import { Link } from "react-router-dom"
import { EditableText } from "../EditableText/EditableText"
import { GoCalendar } from "react-icons/go"
import { ModalInput } from "../Modal/ModalInput/ModalInput"


export const Routines = () => {

    const [routines, setRoutines] = useState<Routine[]>()
    const [isInputOpen, setIsInputOpen] = useState(false)

    useEffect(() => {
        fetchRoutines()
    }, [])

    return (
        <div className="Routines">
            <div className="RoutinesHeader">
                <h2>Рутины</h2>
                <BiPlusCircle size={'25px'} onClick={() => setIsInputOpen(true)}/>
            </div>
            <div className="RoutinesBody">
                {
                    routines != undefined &&
                    <ul className="RoutinesWrapper">
                        {
                            routines.map(routine => (
                                <li className="Routine" key={routine.id}>
                                    <div className="RoutineHeader">
                                        <Link to={routine.id}>
                                            <GoCalendar/>
                                        </Link>
                                        <EditableText tag="div" text={routine.name} onChange={name => updateRoutine(new Routine(routine.id, name, routine.description, routine.periods))}/>
                                    </div>
                                    <BiTrashAlt
                                        className='RemoveIcon'
                                        onClick={() => deleteRoutine(routine.id)}
                                    />
                                </li>
                            ))
                        }
                    </ul>
                }
            </div>
            {
                isInputOpen &&
                <ModalInput
                        placeholder='Введите название рутины'
                        onClose={() => setIsInputOpen(false)}
                        onChange={(routineName) => addRoutine(routineName)}
                />
            }
        </div>
    )

    async function fetchRoutines() {
        const routineSchemas = await httpGet<RoutineSchema[]>('routines')
        const routine = routineSchemas.map(schema => Routine.createFromSchema(schema))
        setRoutines(routine)
    }
    async function addRoutine(name: string) {
        setIsInputOpen(false);
        const newSchema: RoutineSchema = {name: name, periods: [], description: '' }
        const routineSchema = await httpPost<RoutineSchema>('routines', newSchema)
        const routine = Routine.createFromSchema(routineSchema)
        const newRoutines = routines != undefined ? [...routines, routine] : [routine]
        setRoutines(newRoutines)
    }
    async function updateRoutine(changedRoutine: Routine){
        httpPut<RoutineSchema>('routines', changedRoutine.id, changedRoutine.schema)
        const changedRoutines = routines?.map(routine => {
            if(routine.id === changedRoutine.id) return changedRoutine
            else return routine
        })
        setRoutines(changedRoutines)
    }
    function deleteRoutine(routineId: string) {
        httpDelete('routines', routineId)
        const changedRoutines = routines?.filter(routine => routine.id != routineId)
        setRoutines(changedRoutines)
    }
}