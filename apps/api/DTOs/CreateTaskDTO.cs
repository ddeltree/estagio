using System.ComponentModel.DataAnnotations;

public class CreateTaskDTO {
  [Required]
  [MaxLength(200)]
  public string Title { get; set; } = "";

  public Guid UserId { get; set; }
}