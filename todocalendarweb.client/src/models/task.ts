
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
    readonly isComplited: boolean
}

export class Task {

    readonly id: string
    readonly name: string
    readonly description?: string
    readonly periodId: string
    readonly isComplited: boolean
    readonly schema: TaskSchema

    constructor(id : string, name: string, periodId: string,
        isComplited: boolean, description?: string) {
        
        this.id = id
        this.name = name
        this.description = description !== undefined ? description : ''
        this.periodId = periodId
        this.isComplited = isComplited

        this.schema = {
            ...this
        }
    }

    static createFromSchema(schema : TaskSchema) : Task {
        if(!schema.id)
            throw new Error('ID cannot be empty')

        return new Task(schema.id, schema.name, schema.periodId,
            schema.isComplited, schema.description)
    }
}