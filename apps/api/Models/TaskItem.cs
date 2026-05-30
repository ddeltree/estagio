using System.ComponentModel.DataAnnotations;

public class TaskItem {
  public Guid Id { get; set; }

  [Required]
  [MaxLength(200)]
  public string Title { get; set; } = "";

  public bool Completed { get; set; }

  public Guid UserId { get; set; }

  public User User { get; set; } = null!;
}