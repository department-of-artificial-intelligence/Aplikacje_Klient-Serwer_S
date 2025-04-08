using SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public virtual IList<Subject> Subjects {get;set;} = null!;
    public string Title {get;set;} = null!;

}