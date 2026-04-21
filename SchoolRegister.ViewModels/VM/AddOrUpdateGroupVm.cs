using SchoolRegister.Model.DataModels;
using System.ComponentModel.DataAnnotations;
namespace SchoolRegister.ViewModels.VM;
public class AddOrUpdateGroupVm
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
    }