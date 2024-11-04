namespace Models
{
    public class SaveScheduleRequestModel
    {
        public IEnumerable<ScheduleModel> ScheduleModelsList { get; set; }
        public Guid GroupId { get; set; }
    }
}
