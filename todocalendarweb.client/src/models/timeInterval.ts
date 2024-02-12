import { Time } from "./time"

export type TimeIntervalSchema = {

    readonly startTime: string;
    readonly endTime: string;
}

export class TimeInterval {

    
    public readonly startTime : Time;
    public readonly endTime : Time;

    public readonly schema: TimeIntervalSchema;
    
    
    constructor(startTime: Time, endTime: Time) {
        if(startTime.totalMinutes >= endTime.totalMinutes)
            throw new Error('StartTime cannot be greater than or equal to EndTime');
        this.startTime = startTime;
        this.endTime = endTime;

        this.schema = {
            startTime: startTime.toTimeSpanString(), 
            endTime: endTime.toTimeSpanString()
        }
    }

    public static createFromSchema(schema: TimeIntervalSchema) {
        const startTime = Time.createFromTimeSpanFormat(schema.startTime);
        const endTime = Time.createFromTimeSpanFormat(schema.endTime);

        return new TimeInterval(startTime, endTime);
    }

    public isCollideWithTimeInterval(interval: TimeInterval): boolean{

        return this.isIncludesTime(interval.startTime) || 
            this.isIncludesTime(interval.endTime);
    }

    public isCollideWithTimeIntervalSticltly(interval: TimeInterval): boolean{

        return this.isIncludesTimeStictly(interval.startTime) || 
            this.isIncludesTimeStictly(interval.endTime);
    }

    public isIncludesTime(time: Time): boolean{
        return this.startTime.totalMinutes < time.totalMinutes && 
            this.endTime.totalMinutes > time.totalMinutes;
    }

    public isIncludesTimeStictly(time: Time): boolean{
        return this.startTime.totalMinutes <= time.totalMinutes && 
            this.endTime.totalMinutes >= time.totalMinutes;
    }

    public isEqual(interval: TimeInterval): boolean {
        return this.startTime.totalMinutes === interval.startTime.totalMinutes && 
        this.endTime.totalMinutes === interval.endTime.totalMinutes
    }
    
}