using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Index(nameof(Email), IsUnique = true)]
public class User {
  public Guid Id { get; set; }

  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = "";

  [MaxLength(100)]
  public string Email { get; set; } = "";

  public List<TaskItem> Tasks { get; set; } = [];
}
