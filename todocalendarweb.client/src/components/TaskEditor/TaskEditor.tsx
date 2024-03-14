import { Task, TaskCreateDTO } from '../../models/task'
import { EditableText } from '../EditableText/EditableText'
import './style.css';
import { TaskItem } from './TaskItem/TaskItem';
import useTasks from '../../hooks/useTasks';

enum TaskWrapperModes  {
    Editing,
    Executing
}
export default TaskWrapperModes;

interface TaskWrapperProps {
    mode: TaskWrapperModes,
    periodId: string
}

export const TaskEditor = ({
    mode,
    periodId

}: TaskWrapperProps) => {

    const {tasks, createTask, updateTask, deleteTask} = useTasks()

    const handleTaskUpdating = (changedTask: Task) => {
        updateTask(changedTask)
    }

    const handleTaskDeleting = (taskId: string) => {
        deleteTask(taskId)
    }

    async function handleTaskAdding (taskCreateDTO: TaskCreateDTO) {
        createTask(taskCreateDTO)
    }

    return(
        <ul className="TaskWrapper">
            {
                tasks?.map(task => 
                    <TaskItem
                        key={task.id}
                        task={task}
                        mode={mode}
                        onChange={handleTaskUpdating}
                        onRemove={handleTaskDeleting}
                    />
                )
            }
            {
                mode == TaskWrapperModes.Editing &&
                <li className="TaskCreator" key="input">
                    <EditableText
                        key="input"
                        tag='div'
                        placeholder='Введите название задачи'
                        onChange={(text) => 
                            handleTaskAdding({
                                name: text,
                                periodId: periodId
                            })}
                        isFocus={true}
                        isClear={true}
                    />
                </li>
            }
        </ul>
    )
}