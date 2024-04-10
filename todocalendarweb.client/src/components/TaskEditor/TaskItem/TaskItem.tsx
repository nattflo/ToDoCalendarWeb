import { Task } from "../../../models/task"
import { EditableText } from "../../EditableText/EditableText"
import './style.css'
import TaskWrapperModes from "../TaskEditor"
import { TrashCanIcon } from "../../Icons/TrashCanIcon/TrashCanIcon"


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
                    checked={task.isCompleted}
                    onChange={() => onChange(new Task(
                        task.id,
                        task.name,
                        task.periodId,
                        !task.isCompleted,
                        task.description)
                    )}
                />
                :
                <TrashCanIcon
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
                        task.isCompleted,
                        task.description
                    ))}
                />
            }
        </li>
    )
}