using Microsoft.EntityFrameworkCore;

public class TaskService {
  private readonly AppDbContext _db;

  public TaskService(AppDbContext db) {
    _db = db;
  }

  private static TaskDTO ToDto(TaskItem task) {
    return new TaskDTO {
      Id = task.Id,
      Title = task.Title,
      Completed = task.Completed,
      UserId = task.UserId
    };
  }

  public async Task<List<TaskDTO>> GetAllAsync() {
    var tasks = await _db.Tasks.ToListAsync();

    return tasks.Select(ToDto).ToList();
  }

  public async Task<TaskDTO?> GetByIdAsync(Guid id) {
    var task = await _db.Tasks.FindAsync(id);

    return task is null ? null : ToDto(task);
  }

  public async Task<TaskDTO> CreateAsync(CreateTaskDTO dto) {
    var task = new TaskItem {
      Id = Guid.NewGuid(),
      Title = dto.Title,
      UserId = dto.UserId,
      Completed = false
    };

    _db.Tasks.Add(task);

    await _db.SaveChangesAsync();

    return ToDto(task);
  }

  public async Task<TaskDTO?> UpdateAsync(Guid id, UpdateTaskDTO dto) {
    var task = await _db.Tasks.FindAsync(id);

    if (task is null)
      return null;

    task.Title = dto.Title;
    task.Completed = dto.Completed;

    await _db.SaveChangesAsync();

    return ToDto(task);
  }

  public async Task<bool> DeleteAsync(Guid id) {
    var task = await _db.Tasks.FindAsync(id);

    if (task is null)
      return false;

    _db.Tasks.Remove(task);

    await _db.SaveChangesAsync();

    return true;
  }
}