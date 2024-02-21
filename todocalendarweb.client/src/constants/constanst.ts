import { Time } from "../models/time";
import { TimeInterval } from "../models/timeInterval";

export const DAY_START_TIME: Time = Time.createFromHoursAndMinutes(8, 0);
export const DAY_END_TIME: Time = Time.createFromHoursAndMinutes(22, 0);
export const DAY_WORK_INTERVAL: TimeInterval = new TimeInterval(
    Time.createFromHoursAndMinutes(8, 0),
    Time.createFromHoursAndMinutes(22, 0)
)
export const TIME_STEP: Time = Time.createFromHoursAndMinutes(0, 30);
export const TIME_STEPS_COUNT = (DAY_END_TIME.totalMinutes-DAY_START_TIME.totalMinutes)/TIME_STEP.totalMinutes;

export const WORK_DAYS = [
    'Понедельник',
    'Вторник',
    'Среда',
    'Четверг',
    'Пятница',
    'Суббота',
    'Воскресенье'
]