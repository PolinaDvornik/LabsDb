namespace School.MVC.DAL.Models
{
    public class ClassType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Description: {Description};";
        }
    }
}