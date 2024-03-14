import { useState } from "react"
import { Routine, RoutineCreateDTO } from "../../models/routine"
import { BiPlusCircle, BiTrashAlt } from "react-icons/bi"
import { Link } from "react-router-dom"
import { EditableText } from "../EditableText/EditableText"
import { GoCalendar } from "react-icons/go"
import { ModalInput } from "../Modal/ModalInput/ModalInput"
import useRoutines from "../../hooks/useRoutines"
import './style.css'


export const Routines = () => {

    const {routines, createRoutine, updateRoutine, deleteRoutine} = useRoutines()
    const [isInputOpen, setIsInputOpen] = useState(false)

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
                                        <EditableText tag="div" text={routine.name} onChange={name => handleRoutineUpdating(new Routine(routine.id, name, routine.description, routine.periods))}/>
                                    </div>
                                    <BiTrashAlt
                                        className='RemoveIcon'
                                        onClick={() => handleRoutineDeleting(routine.id)}
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
                    onChange={handleRoutineAdding}
                />
            }
        </div>
    )

    function handleRoutineAdding(name: string) {
        setIsInputOpen(false)
        const createDTO: RoutineCreateDTO = {name: name, description: '' }
        createRoutine(createDTO)

    }
    function handleRoutineUpdating(changedRoutine: Routine){
        updateRoutine(changedRoutine)
    }

    function handleRoutineDeleting(routineId: string) {
        deleteRoutine(routineId)
    }
}