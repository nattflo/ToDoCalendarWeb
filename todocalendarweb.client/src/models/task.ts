
export type TaskCreateDTO = {
    readonly name: string
    readonly description?: string
    readonly periodId: string
}

export type TaskSchema = {
    readonly id?: string
    readonly name: string
    readonly description?: string
    readonly periodId: string
    readonly isCompleted: boolean
}

export class Task {

    readonly id: string
    readonly name: string
    readonly description?: string
    readonly periodId: string
    readonly isCompleted: boolean
    readonly schema: TaskSchema

    constructor(id : string, name: string, periodId: string,
        isCompleted: boolean, description?: string) {
        
        this.id = id
        this.name = name
        this.description = description !== undefined ? description : ''
        this.periodId = periodId
        this.isCompleted = isCompleted

        this.schema = {
            ...this
        }
    }

    static createFromSchema(schema : TaskSchema) : Task {
        if(!schema.id)
            throw new Error('ID cannot be empty')

        return new Task(schema.id, schema.name, schema.periodId,
            schema.isCompleted, schema.description)
    }
}