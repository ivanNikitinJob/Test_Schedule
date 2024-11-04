namespace Models
{
    public class SaveScheduleFromFileRequestModel
    {
        public List<SaveScheduleFromFileRequestModelItem> Items { get; set; }
    }

    public class SaveScheduleFromFileRequestModelItem
    {
        public int GroupNumber { get; set; }
        public int DayNumber { get; set; }
        public List<ScheduleFromFileInterval> Intervals { get; set; }
    }

    public class ScheduleFromFileInterval
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
