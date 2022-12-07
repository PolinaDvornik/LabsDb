namespace School.MVC.DTO.Update
{
    public class ScheduleUpdatedDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly StartLessonTime { get; set; }
        public TimeOnly EndLessonTime { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}
