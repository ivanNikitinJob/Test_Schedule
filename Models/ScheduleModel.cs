namespace Models
{
    public class ScheduleModel
    {
        public string Day { get; set; }
        public int DayId { get; set; }
        public Guid? GroupId { get; set; }
        public List<ScheduleIntervalModel> Intervals { get; set; }
    }
}
