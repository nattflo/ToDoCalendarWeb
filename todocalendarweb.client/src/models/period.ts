import { TimeInterval, TimeIntervalSchema } from "./timeInterval"

export type PeriodSchema = {
    readonly id? : string
    readonly name: string
    readonly routineId: string
    readonly dayOfWeek: number
    readonly timePeriod: TimeIntervalSchema
}

export type PeriodCreateDTO = {
    readonly name: string
    readonly routineId: string
    readonly dayOfWeek: number
}

export type PeriodViewModel = {
    readonly id : string
    readonly name: string
    readonly routineId: string
    readonly dayOfWeek: number
    readonly rowStart: number
    readonly rowEnd: number
}

export class PeriodModel {
    readonly id : string;
    readonly name : string;
    readonly routineId: string;
    readonly dayOfWeek: number;
    readonly timeInterval: TimeInterval;

    readonly schema: PeriodSchema;

    constructor(id : string, name: string, routineId: string, dayOfWeek: number, timeInterval: TimeInterval){
        this.timeInterval = timeInterval;
        this.name = name;
        this.routineId = routineId;
        this.dayOfWeek = dayOfWeek;
        this.id = id;

        this.schema = {
            id: id,
            name: name,
            routineId: routineId,
            dayOfWeek: dayOfWeek,
            timePeriod: timeInterval.schema
        }
    }

    static createFromSchema(schema: PeriodSchema) : PeriodModel {
        if(!schema.id)
            throw new Error('ID cannot be empty')

        return new PeriodModel(schema.id, schema.name, schema.routineId, schema.dayOfWeek, TimeInterval.createFromSchema(schema.timePeriod));
    }
}