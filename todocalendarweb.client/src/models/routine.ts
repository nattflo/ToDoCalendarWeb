import { Period, PeriodSchema } from "./period"

export type RoutineSchema = {
    id?: string
    name: string
    periods: PeriodSchema[]
    description: string
}

export class Routine {
    readonly id: string
    readonly name: string
    readonly description: string
    readonly periods: Period[]

    readonly schema: RoutineSchema

    constructor(id : string, name: string, description: string, periods: Period[]) {
        this.id = id
        this.name = name
        this.description = description
        this.periods = periods

        this.schema = {
            id: id,
            name: name,
            description: description,
            periods: periods.map(p => p.schema)
        }
    }

    static createFromSchema(schema: RoutineSchema): Routine{
        if(!schema.id)
            throw new Error('ID cannot be empty')

        return new Routine(
            schema.id,
            schema.name,
            schema.description,
            schema.periods != undefined ? schema.periods.map(p => Period.createFromSchema(p)) : []
        )
    }
}