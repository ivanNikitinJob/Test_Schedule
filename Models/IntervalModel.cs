namespace Models
{
    public class IntervalModel
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
