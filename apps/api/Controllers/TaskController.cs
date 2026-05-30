using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase {
  private readonly TaskService _taskService;

  public TasksController(TaskService taskService) {
    _taskService = taskService;
  }

  [HttpGet]
  public async Task<ActionResult<List<TaskDTO>>> GetAll() {
    var tasks = await _taskService.GetAllAsync();

    return Ok(tasks);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<TaskDTO>> GetById(Guid id) {
    var task = await _taskService.GetByIdAsync(id);

    if (task is null)
      return NotFound();

    return Ok(task);
  }

  [HttpPost]
  public async Task<ActionResult<TaskDTO>> Create([FromBody] CreateTaskDTO dto) {
    var task = await _taskService.CreateAsync(dto);

    return CreatedAtAction(
        nameof(GetById),
        new { id = task.Id },
        task
    );
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult<TaskDTO>> Update(
      Guid id,
      [FromBody] UpdateTaskDTO dto
  ) {
    var task = await _taskService.UpdateAsync(id, dto);

    if (task is null)
      return NotFound();

    return Ok(task);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id) {
    var deleted = await _taskService.DeleteAsync(id);

    if (!deleted)
      return NotFound();

    return NoContent();
  }
}