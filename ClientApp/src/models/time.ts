export class Time {
    
    public readonly hours: number;
    
    public readonly minutes: number;

    public readonly totalMinutes: number;
    
    constructor(minutes: number = 0 , ){
        this.totalMinutes = minutes;

        this.hours = Math.floor(this.totalMinutes/60);
        this.minutes = this.totalMinutes%60;
    }
    public static createFromHoursAndMinutes(hours: number, minutes: number): Time{
        return new Time(hours*60 + minutes);
    }
    public static createFromTimeSpanFormat(timeSpanString : string): Time{
        const [ , hours, minutes] = timeSpanString.split(':').map(Number);
        return this.createFromHoursAndMinutes(hours, minutes);
    }

    public toTimeSpanString(): string {
        return `00:${this.hours >= 10 ?this.hours : '0'+this.hours}:${this.minutes >= 10 ? this.minutes : '0'+this.minutes}`;
    }

    public toString(): string {
        return `${this.hours}:${this.minutes >= 10 ? this.minutes : '0'+this.minutes}`;
    }

    public addMinutes(minutes: number) : Time{
        return new Time(this.totalMinutes + minutes);
    }
}