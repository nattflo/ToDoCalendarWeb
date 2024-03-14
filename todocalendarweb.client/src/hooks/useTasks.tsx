import { useContext } from "react"
import { httpDelete, httpPost, httpPut } from "../utils/http"
import { Task, TaskCreateDTO, TaskSchema } from "../models/task"
import { TasksContext } from "../contexts/TasksContext"

export default function useTasks() {
    const context = useContext(TasksContext)

	if (!context) throw new Error('useTasks must be used within a TasksProvider')
  
	const { tasks, setTasks } = context

    async function createTask(taskCreateDTO: TaskCreateDTO) {
        const taskSchema: TaskSchema = {
            ...taskCreateDTO,
            isComplited: false
        }
        const schema = await httpPost<TaskSchema>('tasks', taskSchema)
        const task = Task.createFromSchema(schema)
        setTasks(prev => [...prev, task])
	}

    function updateTask(task: Task) {
        httpPut<TaskSchema>('routines', task.id, task.schema)
        setTasks(prev => 
        prev.map(t => t.id === task.id ? task : t))
    }

    function deleteTask(taskId: string) {
		httpDelete(`tasks/${taskId}`)
		setTasks(prev => 
		prev.filter(t => t.id !== taskId)
		)
	}

    return {
		tasks,
		createTask,
		updateTask,
		deleteTask
	}
}