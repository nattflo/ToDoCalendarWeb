import { Task, TaskSchema } from '../../models/task'
import { EditableText } from '../EditableText/EditableText'
import './style.css';
import { TaskItem } from './TaskItem/TaskItem';
import { useEffect, useState } from 'react';
import { httpDelete, httpGet, httpPost, httpPut } from '../../utils/http';

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

    const [tasks, setTasks] = useState<Array<Task>>([])

    useEffect(() => {
        fetchTasks();
    }, [])

    const updateTask = (changedTask: Task) => {
        httpPut<Task>('tasks', changedTask.id, changedTask);
        const changedTasks = tasks?.map(task => {
            if(task.id === changedTask.id) return changedTask;
            else return task;
        })
        setTasks(changedTasks);
    }

    const removeTask = (taskId: string) => {
        httpDelete('tasks', taskId)
        const changedTasks = tasks?.filter(task => task.id != taskId);
        setTasks(changedTasks);
    }

    async function addTask (taskSchema: TaskSchema) {
        const newTaskSchema = await httpPost<TaskSchema, TaskSchema>('tasks', taskSchema);
        const task = Task.createFromSchema(newTaskSchema);
        const newTasks : Array<Task> = tasks != undefined ? [...tasks, task] : [task];
        setTasks(newTasks);
    }

    async function fetchTasks () {
        const taskSchemas = await httpGet<Array<TaskSchema>>('periods/'+ periodId +'/tasks');
        const tasks = taskSchemas.map(schema => Task.createFromSchema(schema));
        setTasks(tasks);
    }

    return(
        <ul className="TaskWrapper">
            {
                tasks?.map(task => 
                    <TaskItem
                        key={task.id}
                        task={task}
                        mode={mode}
                        onChange={updateTask}
                        onRemove={removeTask}
                    />
                )
            }
            {
                mode == TaskWrapperModes.Editing &&
                <li className="TaskCreator">
                    <EditableText 
                        tag='div'
                        placeholder='Введите название задачи'
                        onChange={(text) => 
                            addTask({
                                name: text,
                                periodId: periodId,
                                isComplited: false
                            })
                    }/>
                </li>
            }
        </ul>
    )
}