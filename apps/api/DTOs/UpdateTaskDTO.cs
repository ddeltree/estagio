using System.ComponentModel.DataAnnotations;

public class UpdateTaskDTO {
  [Required]
  [MaxLength(200)]
  public string Title { get; set; } = "";

  public bool Completed { get; set; }
}