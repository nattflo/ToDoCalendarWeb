
export type TaskSchema = {
    readonly id? : string;
    readonly name : string;
    readonly description? : string;
    readonly periodId : string;
    readonly isComplited : boolean;
}

export class Task {

    readonly id : string;
    readonly name : string;
    readonly description? : string;
    readonly periodId : string;
    readonly isComplited : boolean;

    constructor(id : string, name: string, periodId: string,
        isComplited: boolean, description?: string) {
        
        this.id = id;
        this.name = name;
        this.description = description !== undefined ? description : '';
        this.periodId = periodId;
        this.isComplited = isComplited;
    }

    static createFromSchema(schema : TaskSchema) : Task {
        if(!schema.id)
            throw new Error('ID cannot be empty')

        return new Task(schema.id, schema.name, schema.periodId,
            schema.isComplited, schema.description)
    }
}