using System.ComponentModel.DataAnnotations;

public class UpdateUserDTO {
  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = "";

  [Required]
  [MaxLength(100)]
  public string Email { get; set; } = "";
}