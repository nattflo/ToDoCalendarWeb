import { Time } from "./time"

export class TimeInterval {

    
    public readonly startTime : Time;

    
    public readonly endTime : Time;
    
    
    constructor(startTime: Time, endTime: Time) {
        if(startTime.totalMinutes >= endTime.totalMinutes)
            throw new Error('StartTime cannot be greater than or equal to EndTime');
        this.startTime = startTime;
        this.endTime = endTime;
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