namespace School.MVC.DAL.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int StudentsCount { get; set; }
        public int CreationYear { get; set; }
        public int ClassroomTeacherId { get; set; }
        public int ClassTypeId { get; set; }
        public Teacher ClassroomTeacher { get; set; }
        public ClassType ClassType { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Number: {Number}, StudentsCount: {StudentsCount}, CreationYear: {CreationYear}, " +
                $"ClassroomTeacher: {ClassroomTeacher.Name} {ClassroomTeacher.Surname}, ClassType: {ClassType.Name}";
        }
    }
}
