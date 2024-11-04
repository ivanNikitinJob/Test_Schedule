namespace Models
{
    public class DayIntervalModel
    {
        public string Day { get; set; }
        public int DayId { get; set; }
        public List<IntervalModel> Intervals { get; set; }
    }
}
