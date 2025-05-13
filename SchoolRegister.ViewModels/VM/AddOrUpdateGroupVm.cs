using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM;

public class AddOrUpdateGroupVm
{
    public int? Id { get; set; } = null;
    public string Name {get; set;}
}
