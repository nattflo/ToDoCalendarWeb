import { Task } from "../../../models/task"
import { BiTrashAlt } from "react-icons/bi"
import { EditableText } from "../../EditableText/EditableText"
import './style.css'
import TaskWrapperModes from "../TaskEditor"


interface TaskItemProps{
    mode: TaskWrapperModes,
    task: Task,
    onChange?: (task: Task) => void
    onRemove?: (taskId: string) => void
}

export const TaskItem = ({
    mode,
    task,
    onChange = () => {},
    onRemove = () => {}
} : TaskItemProps) => {

    return (
        <li className="Task">
            {
                mode === TaskWrapperModes.Executing ?
                <input
                    type='checkbox'
                    checked={task.isComplited}
                    onChange={() => onChange(new Task(
                        task.id,
                        task.name,
                        task.periodId,
                        !task.isComplited,
                        task.description)
                    )}
                />
                :
                <BiTrashAlt
                    onClick={() => onRemove(task.id)}
                />
            }
            {
                mode === TaskWrapperModes.Executing ?
                task.name
                :
                <EditableText
                    tag='div'
                    text={task.name}
                    onChange={(text) => onChange(new Task(
                        task.id,
                        text,
                        task.periodId,
                        task.isComplited,
                        task.description
                    ))}
                />
            }
        </li>
    )
}