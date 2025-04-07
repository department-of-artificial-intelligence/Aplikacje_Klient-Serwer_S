namespace SchoolRegister.Model.DataModels
{
    public class Teacher : User
    {
        public required IList<Subject> Subjects {get; set;}
        public required string Title {get; set;}
    }
}