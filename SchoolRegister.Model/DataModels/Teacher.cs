namespace SchoolRegister.Model.DataModels{
    public class Teacher : User{
        public string Title {get; set;} = null!;
        public IList<Subject> subjects {get; set;} = new List<Subject>();

        public Teacher() {}        
    }
}