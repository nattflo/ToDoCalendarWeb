import { TimeInterval, TimeIntervalSchema } from "./timeInterval";

export type PeriodSchema = {
    readonly id? : string;
    readonly name: string;
    readonly routineId: string;
    readonly dayOfWeek: number;
    readonly timePeriod: TimeIntervalSchema;
}

export class Period {
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

    static createFromSchema(schema: PeriodSchema) : Period {
        if(!schema.id)
            throw new Error('ID cannot be empty')

        return new Period(schema.id, schema.name, schema.routineId, schema.dayOfWeek, TimeInterval.createFromSchema(schema.timePeriod));
    }
}