using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase {
  private readonly AppDbContext _db;

  public UsersController(AppDbContext db) {
    _db = db;
  }

  [HttpGet]
  public async Task<ActionResult<List<UserDTO>>> GetAll() {
    var users = await _db.Users
        .Select(user => new UserDTO {
          Id = user.Id,
          Name = user.Name,
          Email = user.Email
        })
        .ToListAsync();

    return Ok(users);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<UserDTO>> GetById(Guid id) {
    var user = await _db.Users
        .Where(user => user.Id == id)
        .Select(user => new UserDTO {
          Id = user.Id,
          Name = user.Name,
          Email = user.Email
        })
        .FirstOrDefaultAsync();

    if (user is null)
      return NotFound();

    return Ok(user);
  }

  [HttpPost]
  public async Task<ActionResult<UserDTO>> Create(
      [FromBody] CreateUserDTO dto) {
    var user = new User {
      Id = Guid.NewGuid(),
      Name = dto.Name,
      Email = dto.Email
    };

    _db.Users.Add(user);

    await _db.SaveChangesAsync();

    var response = new UserDTO {
      Id = user.Id,
      Name = user.Name,
      Email = user.Email
    };

    return CreatedAtAction(
        nameof(GetById),
        new { id = user.Id },
        response
    );
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult<UserDTO>> Update(
      Guid id,
      [FromBody] UpdateUserDTO dto) {
    var user = await _db.Users.FindAsync(id);

    if (user is null)
      return NotFound();

    user.Name = dto.Name;
    user.Email = dto.Email;

    await _db.SaveChangesAsync();

    return Ok(new UserDTO {
      Id = user.Id,
      Name = user.Name,
      Email = user.Email
    });
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id) {
    var user = await _db.Users.FindAsync(id);

    if (user is null)
      return NotFound();

    _db.Users.Remove(user);

    await _db.SaveChangesAsync();

    return NoContent();
  }
}