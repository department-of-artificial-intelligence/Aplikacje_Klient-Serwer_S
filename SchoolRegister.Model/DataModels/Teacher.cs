using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels;

public class Teacher : User
{
    public IList<Subject> Subject { get; set; } = null;
    public string Title { get; set; } = null!;

}
