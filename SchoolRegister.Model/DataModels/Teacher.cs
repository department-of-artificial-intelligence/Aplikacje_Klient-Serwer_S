using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Teacher
{
    public int Id { get; set; }
    //public List<Subject> Subjects { get; set; } = new List<Subject>();
    public string Title { get; set; }

    [Key]
    public int TeacherId { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }
    public ICollection<Subject> Subjects { get; set; }
}