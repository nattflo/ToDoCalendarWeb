import { Time } from "./time";
import { TimeInterval } from "./timeInterval";

export type PeriodSchema = {
    readonly id? : string;
    readonly name: string;
    readonly startTime: string;
    readonly endTime: string;
}

export class Period {
    readonly id : string;
    readonly name : string;
    readonly timeInterval: TimeInterval;

    readonly schema: PeriodSchema;

    constructor(id : string, name: string, timeInterval: TimeInterval){
        this.timeInterval = timeInterval;
        this.name = name;
        this.id = id;

        this.schema = {
            id: id,
            name: name,
            startTime: timeInterval.startTime.toTimeSpanString(),
            endTime: timeInterval.endTime.toTimeSpanString()
        }
    }

    static createFromSchema(schema: PeriodSchema) : Period {
        if(!schema.id)
            throw new Error('ID cannot be empty')

        const startTime = Time.createFromTimeSpanFormat(schema.startTime);
        const endTime = Time.createFromTimeSpanFormat(schema.endTime);
        const timeInterval = new TimeInterval(startTime, endTime);
        return new Period(schema.id, schema.id ,timeInterval);
    }
}