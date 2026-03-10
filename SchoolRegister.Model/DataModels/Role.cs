using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Role : IdentityRole<int>
{
    public RoleValue RoleValue { get; set; }

    public Role() { }

    public Role(string name, RoleValue roleValue) : base(name)
    {
        RoleValue = roleValue;
    }
}