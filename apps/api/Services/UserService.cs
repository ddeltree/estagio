using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class UserService {
  private readonly AppDbContext _db;

  public UserService(AppDbContext db) {
    _db = db;
  }

  private static readonly Expression<Func<User, UserDTO>> ToDto =
      user => new UserDTO {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email
      };

  public async Task<List<UserDTO>> GetAllAsync() {
    return await _db.Users
        .Select(ToDto)
        .ToListAsync();
  }

  public async Task<UserDTO?> GetByIdAsync(Guid id) {
    return await _db.Users
        .Where(user => user.Id == id)
        .Select(ToDto)
        .FirstOrDefaultAsync();
  }

  public async Task<UserDTO> CreateAsync(CreateUserDTO dto) {
    var emailExists = await _db.Users
        .AnyAsync(user => user.Email == dto.Email);

    if (emailExists)
      throw new InvalidOperationException("Email already exists.");

    var user = new User {
      Id = Guid.NewGuid(),
      Name = dto.Name,
      Email = dto.Email
    };

    _db.Users.Add(user);

    await _db.SaveChangesAsync();

    return await _db.Users
        .Where(u => u.Id == user.Id)
        .Select(ToDto)
        .FirstAsync();
  }

  public async Task<UserDTO?> UpdateAsync(
      Guid id,
      UpdateUserDTO dto) {
    var user = await _db.Users.FindAsync(id);

    if (user is null)
      return null;

    var emailInUse = await _db.Users
        .AnyAsync(u => u.Email == dto.Email && u.Id != id);

    if (emailInUse)
      throw new InvalidOperationException("Email already exists.");

    user.Name = dto.Name;
    user.Email = dto.Email;

    await _db.SaveChangesAsync();

    return await _db.Users
        .Where(u => u.Id == id)
        .Select(ToDto)
        .FirstAsync();
  }

  public async Task<bool> DeleteAsync(Guid id) {
    var user = await _db.Users.FindAsync(id);

    if (user is null)
      return false;

    _db.Users.Remove(user);

    await _db.SaveChangesAsync();

    return true;
  }
}