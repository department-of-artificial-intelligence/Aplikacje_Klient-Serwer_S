using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Text.RegularExpressions;

public class SubjectGroup
{
    public virtual Subject Subject { get; set; } = null!;

    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public int GroupId { get; set; }
}
