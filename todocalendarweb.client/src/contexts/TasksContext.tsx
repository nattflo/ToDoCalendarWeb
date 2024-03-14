import { createContext, useState, useEffect, PropsWithChildren, SetStateAction, Dispatch } from 'react';
import { httpGet } from '../utils/http';
import { Task, TaskSchema } from '../models/task';

export interface TasksContextValue {
    tasks: Task[];
    setTasks: Dispatch<SetStateAction<Task[]>>;
  }

const TasksContext = createContext<TasksContextValue | null>(null)

const TasksProvider = ({ children }: PropsWithChildren) => {
    const [tasks, setTasks] = useState<Task[]>([]);

    useEffect(() => {
        fetchTasks()
    }, [])

    return (
        <TasksContext.Provider value={{ tasks, setTasks }}>
            {children}
        </TasksContext.Provider>
    )

    async function fetchTasks() {
        const taskSchemas = await httpGet<TaskSchema[]>('tasks')
        const tasks = taskSchemas.map(schema => Task.createFromSchema(schema))
        setTasks(tasks);
    }
}

export { TasksContext, TasksProvider };